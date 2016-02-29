#ifndef _EFFECT_H_
#define _EFFECT_H_

#include "Character.h"

class Effect : public Character
{
public:
	Effect(IWorld& world, const GSvector2& position, const int size, const int time);

	virtual void update(float time);

	virtual void draw() const;

private:
	float timer_;
	int effecttime_;
};

#endif