using System;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IContentManager 
	{
		void Initialize(GameType gameType);
		void Initialize(GameType gameType, String root);
		void LoadContent();
	}

	public class ContentManager : IContentManager 
	{
		private readonly IContentFactory contentFactory;
		private String contentRoot;
		private String texturesRoot;

		private const String FONTS_DIRECTORY = "Fonts";
		private const String SOUND_DIRECTORY = "Sound";
		private const String SPRITES_DIRECTORY = "Sprites";
		private const String TILES_DIRECTORY = "Tiles";

		public ContentManager(IContentFactory contentFactory)
		{
			this.contentFactory = contentFactory;
		}

		public void Initialize(GameType gameType)
		{
			Initialize(gameType, String.Empty);
		}
		public void Initialize(GameType gameType, String root)
		{
			contentRoot = String.Format("{0}{1}", root, Constants.CONTENT_DIRECTORY);
			texturesRoot = String.Format("{0}/{1}/{2}/", contentRoot, TILES_DIRECTORY, gameType);
		}

		public void LoadContent()
		{
			//String gameTypeText = MyGame.Manager.StateManager.GameType.ToString();

			//// Fonts.
			//String fontsRoot = String.Format("{0}/{1}/", contentRoot, FONTS_DIRECTORY);
			//Assets.EmulogicFont = contentFactory.LoadFont(fontsRoot + "Emulogic");

			//// Sounds.
			//if (MyGame.Manager.ConfigManager.GlobalConfigData.LoadAudio)
			//{
			//    String soundsRoot = String.Format("{0}/{1}/", contentRoot, SOUND_DIRECTORY);

			//    Assets.SongDictionary = new Dictionary<SongType, Song>();
			//    for (SongType key = SongType.BossMusic1; key <= SongType.GameTitle; ++key)
			//    {
			//        String assetName = String.Format("{0}{1}", soundsRoot, key);
			//        Song value = contentFactory.LoadSong(assetName);
			//        Assets.SongDictionary.Add(key, value);
			//    }

			//    Assets.SoundEffectDictionary = new Dictionary<SoundEffectType, SoundEffectInstance>();
			//    for (SoundEffectType key = SoundEffectType.Aaargh; key <= SoundEffectType.Wrong; ++key)
			//    {
			//        String assetName = String.Format("{0}{1}", soundsRoot, key);
			//        SoundEffectInstance value = contentFactory.LoadSoundEffectInstance(assetName);
			//        Assets.SoundEffectDictionary.Add(key, value);
			//    }
			//}

			// Textures.
			Assets.BlocksTexture = new Texture2D[Constants.NUM_TILES];
			for (BlockType key = BlockType.Blank; key <= BlockType.Strip; key++)
			{
				String assetName = String.Format("{0}{1}", texturesRoot, key);
				Assets.BlocksTexture[(Byte) key] = contentFactory.LoadTexture(assetName);
			}
		}

	}
}
