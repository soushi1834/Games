#include "Bullet.h"
#include "IWorld.h"
#include "DrawSprite2D.h"

Bullet::Bullet(IWorld& world, const GSvector2& position, const GSvector2& velosity) :
Character(world, CHARACTER_PLAYER_BULLET, TEXTURE_PLAYER_BULLET, position, 4.0f)
{
	velosity_ = velosity;
}

void Bullet::update(float time)
{
	position_ += velosity_;

	if (!world_.isInSide(position_))
	{
		alive_ = false;
	}
}

void Bullet::draw() const
{
	static const GSvector2 center(radius_, radius_);
	DrawSprite2D(textureId_, NULL, &center, NULL, 0.0f, &position_, NULL);
}