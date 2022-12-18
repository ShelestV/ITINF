import json
import numpy as np
from scipy.io.wavfile import write

from synthesizer import tone_synthesizer

with open('7_семестр/РСАВІ/5_lb/code/tone_mapping.json', 'r') as f:
    tone_map = json.loads(f.read())

amplitude = 12000
sampling_freq = 44100    

melody = [
    ('F', 0.4), 
    ('D', 0.5), 
    ('F', 0.3), 
    ('C', 0.6), 
    ('F', 0.4),
    ('G', 0.4), 
    ('D', 0.5), 
    ('F', 0.3), 
    ('C', 0.6), 
    ('A', 0.4),
    ('G', 0.4), 
    ('D', 0.5), 
    ('F', 0.3), 
    ('C', 0.6), 
    ('A', 0.4),
]

signal = np.array([])
for item in melody:
    tone_name = item[0]
    freq = tone_map[tone_name]

    duration = item[1]
    
    synthesized_tone = tone_synthesizer(freq, duration, amplitude, sampling_freq)
    signal = np.append(signal, synthesized_tone, axis=0)

print(signal)

write('generated_melody.wav', sampling_freq, signal)