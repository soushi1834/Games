#ifndef _WORLD_CHARACTER_H_
#define _WORLD_CHARACTER_H_

#include "CharacterID.h"
#include "CharacterManager.h"
#include <map>
#include <functional>

class WorldCharacter
{
public:
	WorldCharacter();

	void add(CharacterID id, const CharacterPtr& character);

	void update(float time);

	void draw();

	//消去
	void clear();

	const bool isBossDead();

private:
	//全要素に作用
	void each(std::function<void(CharacterManager& manager)> fn);

	//あたり判定
	void collide(CharacterID id1, CharacterID id2);

private:
	std::map<CharacterID, CharacterManager> managers_;
};

#endif