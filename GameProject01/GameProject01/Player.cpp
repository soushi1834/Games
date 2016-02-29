#include "Player.h"
#include "DrawSprite2D.h"
#include "Screen.h"
#include "IWorld.h"
#include "Bullet.h"
#include "Bomb.h"

Player::Player(IWorld& world, const GSvector2& position) :
Character(world, CHARACTER_PLAYER, TEXTURE_PLAYER, position, 16),
shotcount(0),
bombcount(0),
intervalcount(0),
hp_(3),
bomb_(2),
power_(0)
{

}

void Player::update(float time)
{
	move();
	shot();
	bomb();

	intervalcount--;
	intervalcount = max(intervalcount, 0);

	position_.x = CLAMP(position_.x, WORLD_LEFT_SIDE + radius_, WORLD_RIGHT_SIDE - radius_);
	position_.y = CLAMP(position_.y, WORLD_UP_SIDE + radius_, WORLD_DOWN_SIDE - radius_);
}

void Player::move()
{
	velosity_ = GSvector2(0.0f, 0.0f);
	if (gsGetKeyState(GKEY_LEFT))
	{
		velosity_.x -= 1.0f;
	}
	if (gsGetKeyState(GKEY_RIGHT))
	{
		velosity_.x += 1.0f;
	}
	if (gsGetKeyState(GKEY_UP))
	{
		velosity_.y -= 1.0f;
	}
	if (gsGetKeyState(GKEY_DOWN))
	{
		velosity_.y += 1.0f;
	}
	velosity_.normalize();

	if (gsGetKeyState(GKEY_LSHIFT))
		position_ += velosity_ * 4.0f;
	else
		position_ += velosity_ * 8.0f;
}

void Player::shot()
{
	if (gsGetKeyState(GKEY_Z))
	{
		shotcount += 1;
		if (shotcount % 5 == 0)
		{
			world_.add(CHARACTER_PLAYER_BULLET,
				std::make_shared<Bullet>(world_, position_ + GSvector2(8.0f, 8.0f), velosity_ + GSvector2(0.0f, -12.0f)));
			world_.add(CHARACTER_PLAYER_BULLET,
				std::make_shared<Bullet>(world_, position_ + GSvector2(-8.0f, 8.0f), velosity_ + GSvector2(0.0f, -12.0f)));
			world_.playSE(SE_SHOT);
		}
		if (power_ >= 20)
		{
			if (shotcount % (30 - power_ / 5) == 0)
			{
				world_.add(CHARACTER_PLAYER_BULLET,
					std::make_shared<Bullet>(world_, position_ + GSvector2(8.0f, 8.0f), velosity_ + GSvector2(2.0f, -12.0f)));
				world_.add(CHARACTER_PLAYER_BULLET,
					std::make_shared<Bullet>(world_, position_ + GSvector2(-8.0f, 8.0f), velosity_ + GSvector2(-2.0f, -12.0f)));
			}
		}
		if (power_ >= 40)
		{
			if (shotcount % (30 - power_ / 5) == 0)
			{
				world_.add(CHARACTER_PLAYER_BULLET,
					std::make_shared<Bullet>(world_, position_ + GSvector2(8.0f, 8.0f), velosity_ + GSvector2(4.0f, -12.0f)));
				world_.add(CHARACTER_PLAYER_BULLET,
					std::make_shared<Bullet>(world_, position_ + GSvector2(-8.0f, 8.0f), velosity_ + GSvector2(-4.0f, -12.0f)));
			}
		}
		if (shotcount >= 10000)
			shotcount -= 10000;
	}
	else
	{
		shotcount = 0;
	}
}

void Player::bomb()
{
	if (gsGetKeyTrigger(GKEY_X))
	{
		if (bomb_ > 0 && bombcount == 0) //ステイトパターンを使おう
		{
			bomb_ -= 1;
			bombcount = 2 * 60;
			world_.add(CHARACTER_PLAYER_BOMB,
				std::make_shared<Bomb>(world_, position_, bombcount));
			world_.playSE(SE_BOMB);
		}
	}
	if (bombcount > 0)
	{
		bombcount--;
	}
}

void Player::draw() const
{
	static const GSvector2 center(radius_, radius_);
	if (intervalcount <= 0 || intervalcount % 10 > 2)
	{
		DrawSprite2D(textureId_, NULL, &center, NULL, 0.0f, &position_, NULL);
	}
}

//衝突イベント
void Player::onCollide(Character& other)
{
	switch (other.getCharacterID())
	{
	case CHARACTER_ENEMY:
	case CHARACTER_ENEMY_BOSS:
	case CHARACTER_ENEMY_BULLET:
		if (intervalcount <= 0 &&
			!world_.isBossDead())
		{
			hp_ -= 1;
			if (hp_ <= 0)
			{
				alive_ = false;
				hp_ = max(hp_, 0);
			}
			else
			{
				bomb_ = 2;
				power_ = power_ / 2;
				bombcount += 2 * 60;
				world_.add(CHARACTER_PLAYER_BOMB,
					std::make_shared<Bomb>(world_, position_, bombcount));
				world_.playSE(SE_BOMB);
				intervalcount += 2 * 60;
			}
		}
		break;

	case CHARACTER_ITEM:
		power_ += 1;
		power_ = min(power_, 100);
		break;

	default:
		break;
	}
}

int Player::getHP() const
{
	return hp_;
}

int Player::getBomb() const
{
	return bomb_;
}

int Player::getPower() const
{
	return power_;
}