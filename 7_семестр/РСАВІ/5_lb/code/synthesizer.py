import numpy as np

def tone_synthesizer(freq, duration, amplitude=1.0, sampling_freq=44100):   
    time_axis = np.linspace(0, duration, np.int(duration * sampling_freq))

    signal = amplitude * np.sin(2 * np.pi * freq * time_axis)
    
    scaling_factor = np.power(2, 15) - 1
    signal_normalized = signal / np.max(np.abs(signal))
    signal_scaled = np.int16(signal_normalized * scaling_factor)
    return signal_scaled 

