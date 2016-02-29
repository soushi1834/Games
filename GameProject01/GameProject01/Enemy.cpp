#include "Enemy.h"
#include "DrawSprite2D.h"
#include "IWorld.h"
#include "Item.h"
#include "Effect.h"

Enemy::Enemy(IWorld& world, const GSvector2& position,
	const int move_type, const float start_time,
	const int hp) :
Character(world, CHARACTER_ENEMY, TEXTURE_ENEMY, position, 16.0f),
mode(WAIT),
start_time_(start_time * 60),
move_type_(move_type),
hp_(hp),
e_move(EnemyMove(world, position))
{
}

void Enemy::update(float time)
{
	switch (mode)
	{
	case WAIT:
		if (timer_ >= start_time_)
			mode = START;
		break;

	case START:
		position_ += e_move.move(position_, move_type_);
		if (world_.isInSide(position_))
			mode = STAY;
		break;

	case STAY:
		position_ += e_move.move(position_, move_type_);
		if (!world_.isInSide(position_))
			mode = END;
		break;

	case END:
		alive_ = false;
		break;

	default:
		break;
	}

	timer_ += time;
}

void Enemy::draw() const
{
	static const GSvector2 center(radius_, radius_);
	if (mode != WAIT)
		DrawSprite2D(textureId_, NULL, &center, NULL, 0.0f, &position_, NULL);
}

//è’ìÀÉCÉxÉìÉg
void Enemy::onCollide(Character& other)
{
	if (mode == STAY)
		hp_ -= 1;

	if (hp_ == 0)
	{
		world_.add(CHARACTER_ITEM,
			std::make_shared<Item>(world_, CHARACTER_ITEM, position_, 8));
		world_.add(CHARACTER_EFFECT,
			std::make_shared<Effect>(world_, position_, radius_, 5));
		world_.playSE(SE_BREAK);
		alive_ = false;
	}
}