#ifndef _PLAYER_H_
#define _PLAYER_H_

#include "Character.h"

class Player : public Character{
public:
	Player(IWorld& world, const GSvector2& position);

	virtual void update(float time);

	void move();

	void shot();

	void bomb();

	virtual void draw() const;

	//Õ“ËƒCƒxƒ“ƒg
	virtual void onCollide(Character& other) override;

	int getHP() const;

	int getBomb() const;

	int getPower() const;

private:
	int shotcount;
	int bombcount;
	int intervalcount;
	int hp_;
	int bomb_;
	int power_;
};

#endif