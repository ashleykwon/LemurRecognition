import torch
import matplotlib.pyplot as plt
from torch import nn
import numpy as np
import torchvision
from torchvision import datasets, models, transforms
from PIL import Image
from torchvision.transforms import ToTensor, ToPILImage
import os
from pydantic import BaseModel
import heapq

class LemurSpecies(BaseModel):
    image_path: str
    
# 3. Class for training the model and making predictions
class LemurModel:
    def __init__(self):
        self.model = torch.load('/Users/ashleykwon/Desktop/Run_DNN_on_a_Server/model_06.23.21.pkl', map_location=torch.device('cpu'))
        self.model.eval()

    # 5. Make a prediction based on the user-entered data
    #    Returns the predicted species with its respective probability
    def predict_species(self, image_path):
        data_transforms = transforms.Compose([
        transforms.Resize(256),
        transforms.CenterCrop(224),
        transforms.ToTensor(),
        transforms.Normalize([0.485, 0.456, 0.406], [0.229, 0.224, 0.225])
        ])

        data_dir = image_path
        image = Image.open(data_dir)
       
        class_names = ['black-and-white-ruffed-lemur','blue-eyed-black-lemur','coquerels-sifaka', 'red-ruffed-lemur', 'ring-tailed-lemur']
        image_tensor = data_transforms(image).float()
        image_tensor = image_tensor.unsqueeze_(0)
        output = self.model(image_tensor)
        outputlst = output.data.cpu().numpy()[0]
        top3 = heapq.nlargest(3, range(len(outputlst)), outputlst.take)
        prediction = []
        for idx in top3:
            prediction.append(class_names[idx])
        return prediction

