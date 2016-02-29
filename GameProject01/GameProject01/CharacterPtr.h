#ifndef _CHARACTER_PTR_H_
#define _CHARACTER_PTR_H_

#include <memory>

class Character;
class Player;

typedef std::shared_ptr<Character> CharacterPtr;
typedef std::shared_ptr<Player> PlayerPtr;

#endif