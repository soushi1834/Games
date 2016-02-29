#include "Credit.h"
#include "TextureID.h"
#include "DrawSprite2D.h"
#include <gslib.h>

Credit::Credit()
{

}

Credit::~Credit()
{}

void Credit::initialize()
{
	isEnd_ = false;
	next_ = SCENE_TITLE;

	gsTextureColorKeyMode(GS_TEXCOLOR_KEY_DISABLE);
	gsLoadTexture(TEXTURE_BG, "res/titlebg.bmp");
	gsLoadTexture(TEXTURE_UI, "res/creditlist.bmp");

	sound.addSound(BGM, "res/loop_115.wav", 1);
	sound.addSound(SE_DESIDE, "res/cursor34.wav", 1);
}

void Credit::update(float time)
{
	sound.playSoundLoop(BGM);

	if (gsGetKeyTrigger(GKEY_Z))
	{
		sound.playSound(SE_DESIDE);
		isEnd_ = true;
	}
}

void Credit::draw()
{
	DrawSprite2D(TEXTURE_BG, NULL, NULL, NULL, 0.0f, NULL, NULL);
	DrawSprite2D(TEXTURE_UI, NULL, NULL, NULL, 0.0f, NULL, NULL);
}

bool Credit::isEnd() const
{
	return isEnd_;
}

SceneID Credit::nextScene() const
{
	return next_;
}

void Credit::end() const
{

}