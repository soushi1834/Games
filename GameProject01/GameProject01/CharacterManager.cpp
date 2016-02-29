#include "Character.h"
#include "CharacterManager.h"
#include <algorithm>


CharacterManager::CharacterManager()
{
}

void CharacterManager::add(const CharacterPtr& character)
{
	characters_.push_back(character);
}

void CharacterManager::update(float time)
{
	std::for_each(
		characters_.begin(), characters_.end(),
		[&](CharacterPtr& character) { character->update(time); });
}

void CharacterManager::draw() const
{
	std::for_each(
		characters_.begin(), characters_.end(),
		[](const CharacterPtr& character) { character->draw(); });
}

//�폜
void CharacterManager::remove()
{
	characters_.remove_if(
		[](const CharacterPtr& character) {return character->isDead(); });
}

//�S����
void CharacterManager::clear()
{
	characters_.clear();
}

//�Փ˔���i�L�����j
void CharacterManager::collide(Character& other)
{
	std::for_each(
		characters_.begin(), characters_.end(),
		[&](CharacterPtr& character) { character->collide(other); });
}

//�Փ˔���i�}�l�[�W���[�j
void CharacterManager::collide(CharacterManager& other)
{
	std::for_each(
		characters_.begin(), characters_.end(),
		[&](CharacterPtr& character) { other.collide(*character); });
}

const bool CharacterManager::isCharacterEnpty()
{
	return characters_.empty();
}