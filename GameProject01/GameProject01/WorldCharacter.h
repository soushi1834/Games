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

	//Á‹
	void clear();

	const bool isBossDead();

private:
	//‘S—v‘f‚Éì—p
	void each(std::function<void(CharacterManager& manager)> fn);

	//‚ ‚½‚è”»’è
	void collide(CharacterID id1, CharacterID id2);

private:
	std::map<CharacterID, CharacterManager> managers_;
};

#endif