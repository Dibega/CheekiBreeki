using System;

[Serializable]
public class PlayerResultModel
{
    public string id;
    public uint answerCount;
    public float gameTime;

    public PlayerResultModel(string id, uint answerCount, float gameTime)
    {
        this.id = id;
        this.answerCount = answerCount;
        this.gameTime = gameTime;
    }
}
