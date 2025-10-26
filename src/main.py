# create the main API here

import random
from fastapi import FastAPI, HTTPException
from fastapi.responses import FileResponse
from db_client import supabase
from google import genai
import os
from dotenv import load_dotenv
import json
from elevenlabs.client import ElevenLabs
from elevenlabs import save
from pathlib import Path

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
    prompt = f"""

    You will receive a short positive news headline (for example: "Google advances quantum computing with new echo algorithm").

    Using retrieval and reasoning, expand on this topic by generating one JSON object.

     

    The JSON must have this exact structure:

     

        {{

          "headline": "...",
          "info1": "...",
          "info2": "...",
          "info3": "..."

        }}

     

    Use the snippet provided here: {selected}

    Provide the json structure for each snipped provided. There should be three headlines and 9 info fields in total.

    Make each headline and info field a MAX of 9-10 words.

     

    Return only valid JSON — no markdown, explanations, or extra text.

    """

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
    print(good_news_json)

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

        article_id = selected[id]["id"]
        save(audio, f"./audio_cache/track_{article_id}.mp3")
        article["audio_id"] = article_id

    print(good_news_json)
    return {"good_news": good_news_json}


@app.get("/audio/{audio_id}")
async def get_audio(audio_id: str):
    base_path = Path("./audio_cache/")
    file_path = base_path / f"track_{audio_id}.mp3"
    print(file_path)

    # Check if file exists and is an MP3
    if file_path.exists():
        return FileResponse(file_path, media_type="audio/mpeg", filename=audio_id)
    else:
        raise HTTPException(status_code=404, detail="MP3 file not found")
