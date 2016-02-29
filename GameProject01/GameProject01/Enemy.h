#ifndef _ENEMY_H_
#define _ENEMY_H_

#include "Character.h"
#include "EnemyModeID.h"
#include "EnemyMove.h"
#include <string>

class IWorld;

class Enemy : public Character
{
public:
	Enemy(IWorld& world, const GSvector2& position,
		const int move_type, const float start_time,
		const int hp);

	virtual void update(float time);

	virtual void draw() const;

	//Õ“ËƒCƒxƒ“ƒg
	virtual void onCollide(Character& other) override;

private:
	MODE mode;
	float start_time_;
	int move_type_;
	int hp_;
	EnemyMove e_move;
};

#endif