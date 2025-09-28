using System.Collections.Generic;
using _ProjectBoy.Scripts.Core.CharacterSc;
using YG;

namespace _ProjectBoy.Scripts.Service
{
    public class GamingProgress : IPlayerProgress
    {
        private readonly List<Skin> _skins;

        public GamingProgress(int score, List<Skin> ownSkins, Skin currentSkin)
        {
            Score = score;
            _skins = ownSkins;
            CurrentSkin = currentSkin;
        }

        public Skin CurrentSkin { get; private set; }
        public int Score { get; private set; }

        public bool TryBuySkin(Skin skinObject, out float score)
        {
            if (skinObject.Price <= Score)
            {
                ChangeScore(skinObject.Price, true);
                _skins.Add(skinObject);
                score = Score;
                return true;
            }

            score = Score;
            return false;
        }

        public void AddScore(float score)
        {
            ChangeScore(score, false);
        }

        public void RemoveScore(float score)
        {
            if (Score < score)
            {
                Score = 0;
                return;
            }

            ChangeScore(score, true);
        }

        public void SetSkin(Skin skinObject)
        {
            CurrentSkin = skinObject;
            YG2.SaveProgress();
        }

        public bool HasSkin(Skin skinObject)
        {
            return _skins.Contains(skinObject);
        }

        private void ChangeScore(float score, bool isSubstract)
        {
            if (isSubstract)
                Score -= (int)score;
            else
                Score += (int)score;

            YG2.SetLeaderboard(Constants.LeaderboardName, Score);
            YG2.SaveProgress();
        }
    }
}