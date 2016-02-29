#ifndef _ITEM_H_
#define _ITEM_H_

#include "Character.h"

class Item : public Character
{
public: 
	Item(IWorld& world, const CharacterID characterId, const GSvector2& position, const int size);

	virtual void update(float time);

	virtual void draw() const;

	virtual void onCollide(Character& other) override;

private:

};

#endif