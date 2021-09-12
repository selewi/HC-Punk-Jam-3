using FMOD.Studio;
using FMODUnity;
using HCPJ3.Tools.Extensions;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace HCPJ3
{
    public class AudioManager : MonoBehaviour
    {
        private const string MusicIntensityParam = "Points";

        [EventRef]
        [SerializeField]
        private string _musicEvent = string.Empty;

        [SerializeField]
        private int _maxIntensityScore = 10_000;

        private EventInstance _musicInstance;

        private bool _muted;

        public void Initialize()
        {
            if (_musicInstance.isValid())
            {
                _musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
                _musicInstance.release();
            }

            _musicInstance = RuntimeManager.CreateInstance(_musicEvent);
            SetMusicIntensity(0);
            _musicInstance.start();
        }

        public void SetMusicIntensity(int score)
        {
            score = Mathf.Clamp(score, 0, _maxIntensityScore);
            float parameter = ((float) score).Remap(0, _maxIntensityScore, 0, 1);
            _musicInstance.setParameterByName(MusicIntensityParam, parameter);
        }

        public void MuteAll()
        {
            _muted = !_muted;
            RuntimeManager.MuteAllEvents(_muted);
        }
    }
}
