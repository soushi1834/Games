#ifndef _ENEMY_MOVE_H_
#define _ENEMY_MOVE_H_

#include "EnemyBulletGenerator.h"
#include <gslib.h>

class EnemyMove
{
public:
	EnemyMove(IWorld& world, GSvector2 start);
	~EnemyMove();

	GSvector2 move(GSvector2 position, int type);

	bool moveEnd();

private:
	EnemyBulletGenerator bullet;
	GSvector2 start_;
	GSvector2 vector_;
	int count;
};

#endif