#include "Effect.h"
#include "DrawSprite2D.h"

Effect::Effect(IWorld& world, const GSvector2& position, const int size, const int time) :
Character(world, CHARACTER_EFFECT, TEXTURE_EFFECT, position, size),
timer_(0.0f),
effecttime_(time)
{}

void Effect::update(float time)
{
	alive_ = timer_ <= effecttime_;
	timer_ += 1;
	radius_ += 4;
}

void Effect::draw() const
{
	static const GSvector2 center(32, 32);
	const GSvector2 scale(radius_ / 64, radius_ / 64);
	DrawSprite2D(textureId_, NULL, &center, &scale, 0.0f, &position_, NULL);
}