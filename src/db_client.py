from supabase import create_client, Client
from dotenv import load_dotenv
import os

# database connection setup

load_dotenv()

# Your Supabase project URL and anon/public API key
SUPABASE_URL = "https://ipqslmnjrpjdpkqmvwfs.supabase.co"
SUPABASE_PRIVATE_KEY = os.getenv("SUPABASE_KEY")  # Ensure

# Create the Supabase client instance
supabase: Client = create_client(SUPABASE_URL, SUPABASE_PRIVATE_KEY)
