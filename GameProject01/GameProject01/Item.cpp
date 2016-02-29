#include "Item.h"
#include "IWorld.h"
#include "DrawSprite2D.h"

Item::Item(IWorld& world, const CharacterID characterId, const GSvector2& position, const int size) :
Character(world, characterId, TEXTURE_ITEM, position, size)
{
	velosity_ = GSvector2(0, 0);
}

void Item::update(float time)
{
	position_ += velosity_;
	velosity_ += GSvector2(0, 0.02f);

	if (!world_.isInSide(position_))
	{
		alive_ = false;
	}
}

void Item::draw() const
{
	static const GSvector2 center(radius_, radius_);
	static const GSvector2 scale(radius_ / 8, radius_ / 8);
	DrawSprite2D(textureId_, NULL, &center, &scale, 0.0f, &position_, NULL);
}

void Item::onCollide(Character& other)
{
	if (other.getCharacterID() == CHARACTER_PLAYER)
		alive_ = false;
}