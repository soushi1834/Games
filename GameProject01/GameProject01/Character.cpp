#include "Character.h"

Character::Character(IWorld& world, CharacterID characterId, TextureID textureId, const GSvector2& position, float radius) :
world_(world), characterId_(characterId), textureId_(textureId), position_(position), velosity_(GSvector2(0, 0)), radius_(radius), alive_(true), timer_(0)
{
}

//���S���Ă��邩
bool Character::isDead() const
{
	return !alive_;
}

//�Փ˔���
void Character::collide(Character& other)
{
	if (isCollide(other))
	{
		onCollide(other);
		other.onCollide(*this);
	}
}

//�Փ˂��Ă��邩
bool Character::isCollide(const Character& other) const
{
	return position_.distance(other.position_) <= (radius_ + other.radius_);
}

//�Փ˃C�x���g
void Character::onCollide(Character& other)
{
	alive_ = false;
}

//������Ԃ�
GSvector2 Character::direction(const Character& other) const
{
	auto v = other.position_ - position_;
	return v.normalize();
}

//�ꏊ��Ԃ�
GSvector2 Character::getPosition()
{
	return position_;
}

//�L������ID��Ԃ�
CharacterID Character::getCharacterID()
{
	return characterId_;
}