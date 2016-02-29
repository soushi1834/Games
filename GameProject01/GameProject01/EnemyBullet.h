#ifndef _ENEMY_BULLET_H_
#define _ENEMY_BULLET_H_

#include "Character.h"

class EnemyBullet : public Character
{
public:
	EnemyBullet(IWorld& world, const GSvector2& position, const GSvector2& velosity);

	virtual void update(float time);

	virtual void draw() const;
};

#endif