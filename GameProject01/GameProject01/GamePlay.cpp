#include "GamePlay.h"
#include "DrawSprite2D.h"
#include "TextureID.h"
#include "Screen.h"
#include "CsvReader.h"
#include "Player.h"
#include "Enemy.h"
#include "Boss.h"
#include "World.h"
#include <memory>
#include <gslib.h>

World world;
CsvReader csv;

GamePlay::GamePlay()
{
	world = World(sound);
}

GamePlay::~GamePlay()
{}

void GamePlay::initialize()
{
	isEnd_ = false;
	next_ = SCENE_TITLE;

	//ìßñæâªÇµÇ»Ç¢
	gsTextureColorKeyMode(GS_TEXCOLOR_KEY_DISABLE);
	gsLoadTexture(TEXTURE_PLAYER, "res/player.bmp");
	gsLoadTexture(TEXTURE_PLAYER_BULLET, "res/bullet.bmp");
	gsLoadTexture(TEXTURE_PLAYER_BOMB, "res/bomb.bmp");
	gsLoadTexture(TEXTURE_ENEMY, "res/enemy.bmp");
	gsLoadTexture(TEXTURE_ENEMY_BOSS, "res/boss.bmp");
	gsLoadTexture(TEXTURE_ENEMY_BULLET, "res/e_bullet.bmp");
	gsLoadTexture(TEXTURE_ITEM, "res/item.bmp");
	gsLoadTexture(TEXTURE_UI, "res/òg.bmp");

	//ç∂è„ÇÃêFÇ∆ìØêFÇìßñæâª
	gsTextureColorKeyMode(GS_TEXCOLOR_KEY_AUTO);
	gsLoadTexture(TEXTURE_EFFECT, "res/explosion.bmp");

	sound.addSound(INTRO, "res/sumia.wav", 1);
	sound.addSound(BGM, "res/loop.wav", 1);
	sound.addSound(SE_SHOT, "res/shoot23_a.wav", 4);
	sound.addSound(SE_BOMB, "res/warp02.wav", 1);
	sound.addSound(SE_BREAK, "res/bosu16.wav", 4);
	sound.addSound(SE_BOSS_BREAK, "res/bosu16_1.wav", 1);
	sound.playSoundOnce(INTRO);

	auto player = std::make_shared<Player>(
			world,
			GSvector2((WORLD_RIGHT_SIDE + WORLD_LEFT_SIDE) / 2, (WORLD_DOWN_SIDE + WORLD_UP_SIDE) * 7 / 8)
		);
	world.addPlayer(player);

	csv.load("res/EnemyData_1.csv");
	//ÉwÉbÉ_îÚÇŒÇµÇ≈ì«Ç›çûÇ›
	for (int i = 1; i < csv.getRows(); i++)
	{
		world.add(CHARACTER_ENEMY, std::make_shared<Enemy>(
			world,
			GSvector2(csv.getDataToInteger(i, 0), csv.getDataToInteger(i, 1)),
			csv.getDataToInteger(i, 2),
			csv.getDataToFloat(i, 3),
			csv.getDataToInteger(i, 4)));
	}

	world.add(CHARACTER_ENEMY_BOSS, std::make_shared<Boss>(
		world,
		GSvector2((WORLD_RIGHT_SIDE + WORLD_LEFT_SIDE) / 2, WORLD_UP_SIDE - 33),
		110,
		400));
}

void GamePlay::update(float time)
{
	if (!sound.isPlaying(INTRO))
		sound.playSoundLoop(BGM);

	if (!world.getPlayer()->isDead())
		world.update(time);

	if (world.getPlayer()->isDead() ||
		world.isBossDead())
	{
		if (gsGetKeyTrigger(GKEY_Z))
		{
			sound.stopSound(INTRO);
			sound.stopSound(BGM);
			isEnd_ = true;
		}
	}
}

void GamePlay::draw()
{
	world.draw();
	DrawSprite2D(TEXTURE_UI, NULL, NULL, NULL, 0.0f, NULL, NULL);
	gsFontParameter(1, 24, "ÇlÇr ñæí©");
	gsTextPos(700, 160);
	gsDrawText("Player : %i", world.getPlayerParam()->getHP());
	gsTextPos(700, 192);
	gsDrawText("Bomb   : %i", world.getPlayerParam()->getBomb());
	gsTextPos(700, 224);
	gsDrawText("Power  : %i", world.getPlayerParam()->getPower());

	if (world.getPlayer()->isDead())
	{
		gsFontParameter(1, 64, "ÇlÇr ñæí©");
		gsTextPos(214, 320);
		gsDrawText("Game Over");
		gsFontParameter(1, 32, "ÇlÇr ñæí©");
		gsTextPos(246, 382);
		gsDrawText("Push Z to back");
	}

	if (world.isBossDead())
	{
		gsFontParameter(1, 64, "ÇlÇr ñæí©");
		gsTextPos(182, 320);
		gsDrawText("Game Clear");
		gsFontParameter(1, 32, "ÇlÇr ñæí©");
		gsTextPos(246, 382);
		gsDrawText("Push Z to back");
	}
}

bool GamePlay::isEnd() const
{
	return isEnd_;
}

SceneID GamePlay::nextScene() const
{
	return next_;
}

void GamePlay::end() const
{
	world.clear();
	gsDeleteTexture(TEXTURE_PLAYER);
	gsDeleteTexture(TEXTURE_PLAYER_BULLET);
	gsDeleteTexture(TEXTURE_ENEMY);
	gsDeleteTexture(TEXTURE_ENEMY_BULLET);
	gsDeleteTexture(TEXTURE_ITEM);
	gsDeleteTexture(TEXTURE_UI);
}
