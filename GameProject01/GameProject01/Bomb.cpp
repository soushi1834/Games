#include "Bomb.h"
#include "IWorld.h"
#include "DrawSprite2D.h"

Bomb::Bomb(IWorld& world, const GSvector2& position, const int time) :
Character(world, CHARACTER_PLAYER_BOMB, TEXTURE_PLAYER_BOMB, position, 16.0f),
time_(time)
{}

void Bomb::update(float time)
{
	time_--;
	if (time_ <= 0)
	{
		alive_ = false;
	}

	radius_ += 16;
}

void Bomb::draw() const
{
	static const GSvector2 center(256, 256);
	const GSvector2 scale(radius_ / 256, radius_ / 256);
	DrawSprite2D(textureId_, NULL, &center, &scale, 0.0f, &position_, NULL);
}

void Bomb::onCollide(Character& other)
{

}