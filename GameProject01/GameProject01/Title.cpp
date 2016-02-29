#include "Title.h"
#include "TextureID.h"
#include "DrawSprite2D.h"
#include <gslib.h>

Title::Title() :
phase(PHASE_PUSH_START),
selectScene(GAME_PLAY),
selectStage(STAGE_1)
{}

Title::~Title()
{}

void Title::initialize()
{
	isEnd_ = false;
	next_ = SCENE_GAMEPLAY;
	phase = PHASE_PUSH_START;
	selectScene = GAME_PLAY;

	//“§–¾‰»‚µ‚È‚¢
	gsTextureColorKeyMode(GS_TEXCOLOR_KEY_DISABLE);
	gsLoadTexture(TEXTURE_BG, "res/titlebg.bmp");
	gsLoadTexture(TEXTURE_UI, "res/titlename.bmp");
	gsLoadTexture(TEXTURE_UI_1, "res/pushZ.bmp");
	gsLoadTexture(TEXTURE_UI_2, "res/cursor.bmp");
	gsLoadTexture(TEXTURE_UI_3, "res/gamestart.bmp");
	gsLoadTexture(TEXTURE_UI_4, "res/credit.bmp");

	sound.addSound(BGM, "res/loop_115.wav", 1);
	sound.addSound(SE_SELECT, "res/cursor19.wav", 1);
	sound.addSound(SE_DESIDE, "res/cursor34.wav", 1);
}

void Title::update(float time)
{
	sound.playSoundLoop(BGM);

	switch (phase)
	{
	case PHASE_PUSH_START:
		if (gsGetKeyTrigger(GKEY_Z))
		{
			phase = PHASE_SELECT_SCENE;
			sound.playSound(SE_DESIDE);
		}
		break;

	case PHASE_SELECT_SCENE:
		if (gsGetKeyTrigger(GKEY_Z))
		{
			sound.playSound(SE_DESIDE);
			if (next_ == SCENE_GAMEPLAY)
				sound.stopSound(BGM);
			isEnd_ = true;
		}
		switch (selectScene)
		{
		case GAME_PLAY:
			if (gsGetKeyTrigger(GKEY_UP))
			{
				sound.playSound(SE_SELECT);
				next_ = SCENE_CREDIT;
				selectScene = CREDIT;
			}
			if (gsGetKeyTrigger(GKEY_DOWN))
			{
				sound.playSound(SE_SELECT);
				next_ = SCENE_CREDIT;
				selectScene = CREDIT;
			}
			break;

		case CREDIT:
			if (gsGetKeyTrigger(GKEY_UP))
			{
				sound.playSound(SE_SELECT);
				next_ = SCENE_GAMEPLAY;
				selectScene = GAME_PLAY;
			}
			if (gsGetKeyTrigger(GKEY_DOWN))
			{
				sound.playSound(SE_SELECT);
				next_ = SCENE_GAMEPLAY;
				selectScene = GAME_PLAY;
			}
			break;

		default:
			break;
		}
		
		break;

	default:
		break;
	}
}

void Title::draw()
{
	DrawSprite2D(TEXTURE_BG, NULL, NULL, NULL, 0.0f, NULL, NULL);
	DrawSprite2D(TEXTURE_UI, NULL, NULL, NULL, 0.0f, &GSvector2(128, 128), NULL);
	
	switch (phase)
	{
	case PHASE_PUSH_START:
		DrawSprite2D(TEXTURE_UI_1, NULL, NULL, NULL, 0.0f, &GSvector2(256, 480), NULL);
		break;

	case PHASE_SELECT_SCENE:
		switch (selectScene)
		{
		case GAME_PLAY:
			DrawSprite2D(TEXTURE_UI_2, NULL, NULL, NULL, 0.0f, &GSvector2(320, 448), NULL);
			break;

		case CREDIT:
			DrawSprite2D(TEXTURE_UI_2, NULL, NULL, NULL, 0.0f, &GSvector2(320, 512), NULL);
			break;

		default:
			break;
		}

		DrawSprite2D(TEXTURE_UI_3, NULL, NULL, NULL, 0.0f, &GSvector2(368, 448), NULL);
		DrawSprite2D(TEXTURE_UI_4, NULL, NULL, NULL, 0.0f, &GSvector2(368, 512), NULL);
		break;
	}
}

bool Title::isEnd() const
{
	return isEnd_;
}

SceneID Title::nextScene() const
{
	return next_;
}

void Title::end() const
{
	gsDeleteTexture(TEXTURE_BG);
	gsDeleteTexture(TEXTURE_UI);
	gsDeleteTexture(TEXTURE_UI_1);
	gsDeleteTexture(TEXTURE_UI_2);
	gsDeleteTexture(TEXTURE_UI_3);
	gsDeleteTexture(TEXTURE_UI_4);
}