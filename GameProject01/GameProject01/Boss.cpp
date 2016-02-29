#include "Boss.h"
#include "DrawSprite2D.h"
#include "IWorld.h"
#include "Item.h"
#include "Effect.h"
#include "MathUtility.h"
#include "EnemyBulletID.h"

Boss::Boss(IWorld& world, const GSvector2& position,
	const float start_time,
	const int hp) :
Character(world, CHARACTER_ENEMY_BOSS, TEXTURE_ENEMY_BOSS, position, 32.0f),
mode(WAIT),
start_time_(start_time * 60),
hp_(hp),
maxhp(hp),
bullet(EnemyBulletGenerator(world)),
start_(position),
vector_(GSvector2(0, 0)),
count(0),
degree(0)
{

}

void Boss::update(float time)
{
	switch (mode)
	{
	case WAIT:
		if (timer_ >= start_time_)
			mode = START;
		break;

	case START:
		appear();
		if (appear())
		{
			count = 0;
			mode = STAY;
		}
		break;

	case STAY:
		move();
		break;

	default:
		break;
	}

	timer_ += time;
}

bool Boss::appear()
{
	position_ = MathUtility::interpolatePower(start_, start_ + GSvector2(0, 192), count / 180.0f, 0.5f);
	count++;
	return count >= 180;
}

void Boss::move()
{
	vector_ = GSvector2(MathUtility::cos(degree) * 4, MathUtility::sin((degree + 45) * 2));

	if (hp_ >= maxhp * 3 / 4)
	{
		if (count < 240)
		{
			if (count % 60 == 0)
			{
				bullet.fire(position_, ÇQÇSï˚à , 2);
			}
		}
		else
		{
			if (count % 2 == 0)
				bullet.fire(position_, ÇŒÇÁÇ‹Ç´, 4);
			if (count >= 300)
			{
				count = -60;
			}
		}
	}
	else if (hp_ >= maxhp / 2)
	{
		if (count % 150 == 0)
		{
			bullet.fire(position_, êÇíºíe, 4);
		}
		if (count % 40 == 0)
		{
			for (int i = 0; i < 8; i++)
			{
				bullet.fire(position_, ÇŒÇÁÇ‹Ç´, 4);
			}
		}
	}
	else if (hp_ >= maxhp / 4)
	{
		
		if (count % 40 == 0)
		{
			bullet.fire(position_, é©ã@ë_Ç¢íe, 4);
		}
		if (count % 40 == 0)
		{
			for (int i = 0; i < 8; i++)
			{
				bullet.fire(position_, ÇŒÇÁÇ‹Ç´, 4);
			}
		}
	}
	else
	{
		if (count % 60 == 0)
		{
			bullet.fire(position_, é©ã@ë_Ç¢ÇQÇSï˚à , 4);
			
		}
		if (count % 5 == 0)
		{
			bullet.fire(position_, ÇŒÇÁÇ‹Ç´, 4);
		}
	}

	position_ += vector_;
	count++;
	degree++;
	if (degree >= 360)
		degree -= 360;
}

void Boss::draw() const
{
	static const GSvector2 center(radius_, radius_);
	if (mode != WAIT)
		DrawSprite2D(textureId_, NULL, &center, NULL, 0.0f, &position_, NULL);
}

void Boss::onCollide(Character& other)
{
	if (mode == STAY)
		hp_ -= 1;

	if (hp_ <= 0)
	{
		world_.add(CHARACTER_EFFECT,
			std::make_shared<Effect>(world_, position_, radius_, 30));
		world_.playSE(SE_BOSS_BREAK);
		alive_ = false;
	}
}