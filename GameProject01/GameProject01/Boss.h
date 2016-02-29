#ifndef _BOSS_H_
#define _BOSS_H_

#include "Character.h"
#include "EnemyModeID.h"
#include "EnemyBulletGenerator.h"
#include <gslib.h>

class IWorld;

class Boss : public Character
{
public:
	Boss(IWorld& world, const GSvector2& position,
		const float start_time, 
		const int hp);

	virtual void update(float time);

	bool appear();

	void move();

	virtual void draw() const;

	//è’ìÀÉCÉxÉìÉg
	virtual void onCollide(Character& other) override;

private:
	MODE mode;
	float start_time_;
	int hp_;
	int maxhp;
	EnemyBulletGenerator bullet;
	GSvector2 start_;
	GSvector2 vector_;
	int count;
	int degree;
};

#endif