#include "World.h"
#include "Character.h"
#include "Player.h"
#include "Screen.h"

World::World()
{
	se_ = WaveSound();
}

World::World(WaveSound se)
{
	se_ = se;
}

World::~World()
{
}

void World::update(float time)
{
	characters_.update(time);
}

void World::draw()
{
	characters_.draw();
}

//消去
void World::clear()
{
	characters_.clear();
	player_ = nullptr;
}

//キャラクターの追加
void World::add(CharacterID id, const CharacterPtr& character)
{
	characters_.add(id, character);
}

//プレイヤーの追加
void World::addPlayer(const PlayerPtr& player)
{
	player_ = player;
	playerParam_ = player;
	add(CHARACTER_PLAYER, player);
}

//プレイヤーの取得
const CharacterPtr& World::getPlayer() const
{
	return player_;
}

const PlayerPtr& World::getPlayerParam() const
{
	return playerParam_;
}

//画面内にいるかどうか
bool World::isInSide(const GSvector2& position) const
{
	static const float WORLD_SIZE = 32;

	return (position.x > WORLD_LEFT_SIDE - WORLD_SIZE
		&& position.y > WORLD_UP_SIDE - WORLD_SIZE
		&& position.x < (WORLD_RIGHT_SIDE + WORLD_SIZE)
		&& position.y < (WORLD_DOWN_SIDE + WORLD_SIZE));
}

const bool World::isBossDead()
{
	return characters_.isBossDead();
}

void World::playSE(SoundID id)
{
	gsPlaySE(id);
}