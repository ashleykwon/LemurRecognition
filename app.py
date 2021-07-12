# 1. Library imports
import uvicorn
from fastapi import FastAPI, Request, Response
from pydantic import BaseModel
from Model import LemurModel, LemurSpecies
import torch
import io, json

# 2. Create app and model objects
app = FastAPI()
model = LemurModel()

class Input(BaseModel):
    image_path: str

@app.put("/predict")
def predict(d:Input):
    filePath = d.image_path
    print(filePath)
    predictions = model.predict_species(
       filePath
    )
    jsonData = json.dumps(predictions)
    return jsonData
    
# Run the API with uvicorn
#    Will run on http://127.0.0.1:8000
if __name__ == '__main__':
    uvicorn.run(app, host='127.0.0.1', port=8000)