using System;
using UnityEngine;

/// <summary>
/// Represents a single news item with headline, facts, and audio identifier.
/// </summary>
[Serializable]
public class NewsItem
{
    public string headline;
    public string fact1;
    public string fact2;
    public string fact3;
    public string audioClipId;
}

