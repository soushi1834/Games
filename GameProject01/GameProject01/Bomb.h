#ifndef _BOMB_H_
#define _BOMB_H_

#include "Character.h"

class Bomb : public Character
{
public:
	Bomb(IWorld& world, const GSvector2& position, const int time);

	virtual void update(float time);

	virtual void draw() const;

	//Õ“ËƒCƒxƒ“ƒg
	virtual void onCollide(Character& other) override;

private:
	int time_;
};

#endif