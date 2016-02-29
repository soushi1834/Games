#include "EnemyBullet.h"
#include "IWorld.h"
#include "DrawSprite2D.h"

EnemyBullet::EnemyBullet(IWorld& world, const GSvector2& position, const GSvector2& velosity) :
Character(world, CHARACTER_ENEMY_BULLET, TEXTURE_ENEMY_BULLET, position, 4.0f)
{
	velosity_ = velosity;
}

void EnemyBullet::update(float time)
{
	position_ += velosity_;

	if (!world_.isInSide(position_))
	{
		alive_ = false;
	}
}

void EnemyBullet::draw() const
{
	static const GSvector2 center(radius_, radius_);
	DrawSprite2D(textureId_, NULL, &center, NULL, 0.0f, &position_, NULL);
}