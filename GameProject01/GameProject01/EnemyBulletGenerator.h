#ifndef _ENEMY_BULLET_GENERATOR_H_
#define _ENEMY_BULLET_GENERATOR_H_

#include <GSvector2.h>

class IWorld;

class EnemyBulletGenerator
{
public:
	EnemyBulletGenerator(IWorld& world);

	void fire(const GSvector2& position, const int bullet_type, const float speed);

private:
	IWorld& world_;
	int count;
};

#endif