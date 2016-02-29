#ifndef _BULLET_H_
#define _BULLET_H_

#include "Character.h"

class Bullet : public Character
{
public:
	Bullet(IWorld& world, const GSvector2& position, const GSvector2& velosity);

	virtual void update(float time);

	virtual void draw() const;
};

#endif