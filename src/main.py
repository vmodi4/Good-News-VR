# create the main API here

import random
from fastapi import FastAPI, requests, HTTPException
from fastapi.responses import FileResponse
from db_client import supabase
from google import genai
import os
from dotenv import load_dotenv
import json
from elevenlabs.client import ElevenLabs

load_dotenv()

api = os.getenv("GEMINI_API_KEY")
app = FastAPI()
# this will track which have been used- regardless of the function call
used_ids = set()


@app.get("/")
async def get_good_news():
    all_snippets = supabase.table("Snippets").select("*").execute()

    snippets = [
        snippet for snippet in all_snippets.data if snippet["id"] not in used_ids
    ]

    selected = random.sample(snippets, min(3, len(snippets)))
    for snippet in selected:
        used_ids.add(snippet["id"])

    # need to only get 2-3 snippets, load them in the background

    # now select 3 random snippets

    # selected = random.sample(all_snippets, min(3, len(all_snippets)))
    # fix as this prob not effcient, could be red flag if they look at code- also don'

    # now create prompt

    prompt = f"If I just give you a snippet of good news- like a headline(for example: Google has progressed in Quantum computing by creating an echo algorithm- could you perform RAG and get more information regarding this by making an output that is in JSON- and since JSON follows key value semantics here is an example: headline: - this will the first key, it will take the snippet of good information given and make it sound more good, the second key, third, and fourth key will represent more information - and each key will only have one extra sentence of information(5-10 words). Also, the headline should be around the same length. Here are the snippets: {selected}  "
    # try this for now.

    client = genai.Client(api_key=api)
    response = client.models.generate_content(
        model="gemini-2.5-flash",
        contents=f"""{prompt} """,
    )

    my_string = response.text

    clean_text = my_string.strip("` \n")

    if clean_text.lower().startswith("json"):
        clean_text = clean_text[4:].strip()

    good_news_json = json.loads(clean_text)

    ## SAMPLE RETURN
    #     "headline": "Google achieves a significant breakthrough in Quantum Computing with an innovative echo algorithm.",
    # "more_info_1": "This advancement dramatically improves the stability of quantum systems.",
    # "more_info_2": "The echo algorithm efficiently corrects crucial quantum errors.",
    # "more_info_3": "It brings us closer to practical, fault-tolerant quantum computers."

    elevenlabs = ElevenLabs(
        api_key=os.getenv("ELEVENLABS_API_KEY"),
    )

    for id, article in enumerate(good_news_json):
        prompt = ""
        for field in article.values():
            prompt += field

        audio = elevenlabs.text_to_speech.convert(
            text=prompt,
            voice_id="JBFqnCBsd6RMkjVDRZzb",
            model_id="eleven_multilingual_v2",
            output_format="mp3_44100_128",
        )

        arcticle_id = selected[id]["id"]
        with open(f"./audio_cache/{arcticle_id}.mp3") as audio_file:
            audio_file.write(audio)

    return {"good_news": good_news_json}


@app.get("/audio_cache/{audio_id}")
async def get_audio(audio_id: str):
    file_path = f"./audio_cache/{audio_id}.mp3"

    # Check if file exists and is an MP3
    if file_path.exists() and file_path.suffix == ".mp3":
        return FileResponse(file_path, media_type="audio/mpeg", filename=audio_id)
    else:
        raise HTTPException(status_code=404, detail="MP3 file not found")


# two helper functions:

# first will call the database client t o

# print("Database client imported successfully")
