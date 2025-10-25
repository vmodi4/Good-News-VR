# create the main API here

import random
from fastapi import FastAPI, requests
from db_client import supabase
from google import genai
import os
from dotenv import load_dotenv
import json

load_dotenv() 

api = os.getenv("GEMINI_API_KEY")




app = FastAPI()

used_ids = set()  
# this will track which have been used- regardless of the function call

@app.get("/")
async def get_good_news():
    all_snippets = supabase.table("Snippets").select("*").execute()
    
    snippets = [snippet for snippet in all_snippets.data if snippet['id'] not in used_ids]

    selected = random.sample(snippets, min(3, len(snippets)))
    for snippet in selected:
        used_ids.add(snippet['id'])

        
    
    
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

    return {"good_news": good_news_json}


# two helper functions:

# first will call the database client t o

#print("Database client imported successfully")
