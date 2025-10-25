from db_client import supabase
import os

from dotenv import load_dotenv

load_dotenv()


response = supabase.table("Snippets").select("*").execute() 
# simple query to test connection.



print(response.data)
