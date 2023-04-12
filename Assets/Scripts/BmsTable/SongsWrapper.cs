using System;
using System.Collections.Generic;

[Serializable]
public class SongsWrapper
{
    public List<Song> songs;

    public SongsWrapper(List<Song> songs)
    {
        this.songs = songs;
    }
}
