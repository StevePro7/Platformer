using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;

namespace WindowsGame.Common.Managers
{
	public interface ISoundManager 
	{
		void Initialize();
		void Initialize(Boolean playAudio);

		void PlayMusic();
		void PlayMusic(Boolean isRepeating);
		void PauseMusic();
		void ResumeMusic();
		void StopMusic();

		void PlaySoundEffect(SoundEffectType key);
		void StopSoundEffect(SoundEffectType key);

		void SetPlayAudio(Boolean playAudio);

		Boolean PlayAudio { get; }
	}

	public class SoundManager : ISoundManager
	{
		private readonly ISoundFactory soundFactory;

		public SoundManager(ISoundFactory soundFactory)
		{
			this.soundFactory = soundFactory;
		}

		public void Initialize()
		{
			Initialize(MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio);
		}

		public void Initialize(Boolean playAudio)
		{
			PlayAudio = playAudio;
		}

		public void PlaySoundEffect(SoundEffectType key)
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			if (!PlayAudio)
			{
				return;
			}

			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.PlaySoundEffect(value);
		}

		public void StopSoundEffect(SoundEffectType key)
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			if (!PlayAudio)
			{
				return;
			}

			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.StopSoundEffect(value);
		}

		public void PlayMusic()
		{
			PlayMusic(false);
		}

		public void PlayMusic(Boolean isRepeating)
		{
			if (null == Assets.GameSong)
			{
				return;
			}

			SetVolume();
			soundFactory.PlayMusic(Assets.GameSong, isRepeating);
		}

		public void PauseMusic()
		{
			soundFactory.PauseMusic();
		}

		public void ResumeMusic()
		{
			SetVolume();
			soundFactory.ResumeMusic();
		}

		public void StopMusic()
		{
			soundFactory.StopMusic();
		}

		private void PauseAllAudio()
		{
			PauseMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.PauseSoundEffect(soundEffect);
			}
		}

		private void ResumeAllAudio()
		{
			ResumeMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.ResumeSoundEffect(soundEffect);
			}
		}

		private void SetVolume()
		{
			if (PlayAudio)
			{
				soundFactory.SetMaxVolume();
			}
			else
			{
				soundFactory.SetMinVolume();
			}
		}

		public void SetPlayAudio(Boolean playAudio)
		{
			PlayAudio = playAudio;
			SetVolume();
		}

		public Boolean PlayAudio { get; private set; }
	}
}
