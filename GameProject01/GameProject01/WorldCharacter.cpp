#include "WorldCharacter.h"

WorldCharacter::WorldCharacter()
{
}

void WorldCharacter::add(CharacterID id, const CharacterPtr& character)
{
	managers_[id].add(character);
}

void WorldCharacter::update(float time)
{
	each([&](CharacterManager& manager) {manager.update(time); });
	collide(CHARACTER_PLAYER, CHARACTER_ENEMY);
	collide(CHARACTER_PLAYER, CHARACTER_ENEMY_BOSS);
	collide(CHARACTER_PLAYER, CHARACTER_ENEMY_BULLET);
	collide(CHARACTER_PLAYER, CHARACTER_ITEM);
	collide(CHARACTER_PLAYER_BOMB, CHARACTER_ENEMY);
	collide(CHARACTER_PLAYER_BOMB, CHARACTER_ENEMY_BULLET);
	collide(CHARACTER_ENEMY, CHARACTER_PLAYER_BULLET);
	collide(CHARACTER_ENEMY_BOSS, CHARACTER_PLAYER_BULLET);
	each([](CharacterManager& manager) {manager.remove(); });
}

void WorldCharacter::draw()
{
	each([&](CharacterManager& manager) {manager.draw(); });
}

//全消去
void WorldCharacter::clear()
{
	managers_.clear();
}

const bool WorldCharacter::isBossDead()
{
	return managers_[CHARACTER_ENEMY_BOSS].isCharacterEnpty();
}

//全要素に適用
void WorldCharacter::each(std::function<void(CharacterManager& manager)> fn)
{
	for (auto i = managers_.begin(); i != managers_.end(); ++i)
	{
		fn(i->second);
	}
}

//あたり判定
void WorldCharacter::collide(CharacterID id1, CharacterID id2)
{
	managers_[id1].collide(managers_[id2]);
}