#include "Character.h"

Character::Character(IWorld& world, CharacterID characterId, TextureID textureId, const GSvector2& position, float radius) :
world_(world), characterId_(characterId), textureId_(textureId), position_(position), velosity_(GSvector2(0, 0)), radius_(radius), alive_(true), timer_(0)
{
}

//死亡しているか
bool Character::isDead() const
{
	return !alive_;
}

//衝突判定
void Character::collide(Character& other)
{
	if (isCollide(other))
	{
		onCollide(other);
		other.onCollide(*this);
	}
}

//衝突しているか
bool Character::isCollide(const Character& other) const
{
	return position_.distance(other.position_) <= (radius_ + other.radius_);
}

//衝突イベント
void Character::onCollide(Character& other)
{
	alive_ = false;
}

//方向を返す
GSvector2 Character::direction(const Character& other) const
{
	auto v = other.position_ - position_;
	return v.normalize();
}

//場所を返す
GSvector2 Character::getPosition()
{
	return position_;
}

//キャラのIDを返す
CharacterID Character::getCharacterID()
{
	return characterId_;
}