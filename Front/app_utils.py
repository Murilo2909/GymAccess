import streamlit as st
import requests
import base64
import json
import urllib3
from io import BytesIO

# CONFIG
API_BASE = "https://localhost:5052"  # altere se necessário (http ou https)
VERIFY_SSL = False  # em dev local com self-signed certs
SAMPLE_IMAGE_PATH = r"/mnt/data/61c2fdf1-8d85-4c73-bd63-a4f2e4480fa3.png"

if not VERIFY_SSL:
    urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# Auth helpers

def get_token():
    return st.session_state.get("token")


def set_token(token: str):
    st.session_state["token"] = token


def auth_headers():
    token = get_token()
    if not token:
        return {}
    return {"Authorization": f"Bearer {token}"}

# HTTP helpers that attach Authorization automatically

def req_get(url, **kwargs):
    headers = kwargs.pop("headers", {})
    headers.update(auth_headers())
    return requests.get(url, verify=False, headers=headers, **kwargs)


def req_post(url, **kwargs):
    headers = kwargs.pop("headers", {})
    headers.update(auth_headers())
    return requests.post(url, verify=False, headers=headers, **kwargs)


def req_put(url, **kwargs):
    headers = kwargs.pop("headers", {})
    headers.update(auth_headers())
    return requests.put(url, verify=False, headers=headers, **kwargs)


def req_delete(url, **kwargs):
    headers = kwargs.pop("headers", {})
    headers.update(auth_headers())
    return requests.delete(url, verify=False, headers=headers, **kwargs)

# Utilities

def bytes_to_base64(b: bytes) -> str:
    return base64.b64encode(b).decode("utf-8")


def show_json(obj):
    st.code(json.dumps(obj, indent=2, ensure_ascii=False), language="json")

