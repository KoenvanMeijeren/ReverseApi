﻿namespace ReversiApi.Model;

public interface IPlayer
{
    public string Token { get; }
    public Color Color { get; }
    
}