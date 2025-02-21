 <div align="center">

<!-- logo -->
![image](https://github.com/user-attachments/assets/29264388-52d8-4423-8e07-dda41cc7569b)

## 던전 앤 파이트

[<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=Github&logoColor=white" />]() [<img src="https://img.shields.io/badge/Notion-white?style=flat&logo=notion&logoColor=black" />]() [<img src="https://img.shields.io/badge/Figma-F24E1E?style=flat&logo=figma&logoColor=white" />]()
<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.06~2025.02.13-73abf0?style=flat&logo=&logoColor=white" />]()

</div> 

## ?? 프로젝트 소개
C#을 사용하여 제작한 텍스트 기반 RPG 콘솔 게임입니다.  
플레이어는 던전을 탐험하고, 몬스터와 전투를 벌이며, 아이템을 획득하고 성장할 수 있습니다.
<br />

## ?? 게임 기능 개요

| 기능 | 설명 |
|---|---|
| ?? **마을** | 처음 시작하거나 플레이어가 죽으면 새 캐릭터를 생성합니다. <br> - **이름과 직업**을 입력받습니다. <br> - **메뉴 선택**으로 다른 기능으로 이동할 수 있습니다. |
| ?? **상태 보기** | 현재 캐릭터의 <strong>정보(레벨, 장비, 능력치 등)</strong>를 표시합니다. |
| ? **전투 시작** | 특정 층의 **던전에서 전투**를 시작할 수 있습니다. <br> - 층이 높아질수록 **몬스터 레벨 증가** ?? <br> - **몬스터마다 보상**이 다름 ?? <br> - 플레이어 턴에는 **공격, 스킬, 포션 사용, 도망치기** 중 선택 가능 |
| ? **층 이동하기** | 클리어 기록이 있는 던전으로 **다시 이동하여 플레이 가능**합니다. |
| ?? **인벤토리** | 던전 클리어, 퀘스트 성공 시 **획득한 아이템과 포션**을 확인합니다. <br> - **아이템은 장착 부위마다 하나씩 장착 가능** ?? <br> - **아이템 개수가 많아지면 페이지 넘김 지원** ?? |
| ?? **퀘스트 수주** | - 아직 받지 않은 **퀘스트를 확인하고 수락/거절 가능** ? <br> - **진행 중인 퀘스트 & 완료한 퀘스트 목록**을 확인 가능 ? |
| ? **게임 종료** | **마을에서 게임을 종료**할 수 있습니다. |

<br />  

---

## ?? 화면 구성
|마을 씬|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Main.png?raw=true" width="450"/>|
|마을에서 다양한 활동을 시작할 수 있습니다.|

|던전 전투 씬|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Battle.png?raw=true" width="450"/>|
|던전의 층마다 다른 종류의 몬스터들과의 전투가 벌어집니다.|  

|인벤토리 씬|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Inventory.png?raw=true" width="450"/>|
|인벤토리에서 획득한 장비를 확인하고 장착/해제할 수 있습니다.|

|퀘스트 수주 씬|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Quest.png?raw=true" width="450"/>|
|퀘스트를 수락하고 진행 상황을 확인할 수 있습니다.|

<br />   

---

## ?? 프로젝트 폴더 구조
```
??Dun9eonAndFi9ht
 ┣ ??Dun9eonAndFi9ht
 ┃ ┣ ??App
 ┃ ┃ ┗ ??Program.cs
 ┃ ┣ ??Characters
 ┃ ┃ ┣ ??Character.cs
 ┃ ┃ ┣ ??Monster.cs
 ┃ ┃ ┗ ??Player.cs
 ┃ ┣ ??DataBase
 ┃ ┃ ┣ ??enemy_stage1.json
 ┃ ┃ ┣ ??enemy_stage2.json
 ┃ ┃ ┣ ??enemy_stage3.json
 ┃ ┃ ┣ ??enemy_stage4.json
 ┃ ┃ ┣ ??enemy_stage5.json
 ┃ ┃ ┣ ??item.json
 ┃ ┃ ┣ ??job.json
 ┃ ┃ ┣ ??monsterSkill.json
 ┃ ┃ ┣ ??player.json
 ┃ ┃ ┣ ??playerExpTable.json
 ┃ ┃ ┣ ??playerSkill.json
 ┃ ┃ ┣ ??potion.json
 ┃ ┃ ┗ ??questTable.json
 ┃ ┣ ??Item
 ┃ ┃ ┣ ??Item.cs
 ┃ ┃ ┗ ??Potion.cs
 ┃ ┣ ??Manager
 ┃ ┃ ┣ ??DataTableManager.cs
 ┃ ┃ ┣ ??GameManager.cs
 ┃ ┃ ┣ ??InventoryManager.cs
 ┃ ┃ ┣ ??QuestManager.cs
 ┃ ┃ ┗ ??SkillManager.cs
 ┃ ┣ ??Quests
 ┃ ┃ ┣ ??EquipItemQuest.cs
 ┃ ┃ ┣ ??KillMonsterQuest.cs
 ┃ ┃ ┣ ??Quest.cs
 ┃ ┃ ┗ ??ReachLevelQuest.cs
 ┃ ┣ ??Scenes
 ┃ ┃ ┣ ??Dungeon.cs
 ┃ ┃ ┣ ??InventoryScene.cs
 ┃ ┃ ┣ ??MoveStage.cs
 ┃ ┃ ┣ ??PlayerStat.cs
 ┃ ┃ ┣ ??QuestScene.cs
 ┃ ┃ ┣ ??Scene.cs
 ┃ ┃ ┗ ??StartScene.cs
 ┃ ┣ ??Skill
 ┃ ┃ ┣ ??AlphaStrike.cs
 ┃ ┃ ┣ ??DoubleStrike.cs
 ┃ ┃ ┣ ??Fireball.cs
 ┃ ┃ ┣ ??IceSpear.cs
 ┃ ┃ ┣ ??MonsterSkill.cs
 ┃ ┃ ┣ ??SkillBase.cs
 ┃ ┃ ┣ ??SlashFrenzy.cs
 ┃ ┃ ┗ ??VitalStrike.cs
 ┃ ┣ ??StaticClass
 ┃ ┃ ┗ ??Utility.cs
 ┃ ┣ ??System
 ┃ ┃ ┗ ??BattleSystem.cs
 ┃ ┣ ??DataDefinition.cs
 ┃ ┗ ??Dun9eonAndFi9ht.csproj
```

---


## ?? 프로젝트 계획 단계

### **?? 아이디어 구상**  
첫 회의를 통해 게임의 핵심 구조를 설계하였으며, 진행 방식과 역할을 논의하며 프로젝트의 방향성을 구체화하였습니다.  
<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Figma.png?raw=true" width="450"/>
<br /><br />
### **?? 협업 과정**  
원활한 협업을 위해 GitHub을 활용하여 코드 형상 관리를 진행하였으며, Notion을 이용해 문서를 정리하고 일정 및 업무 진행 상황을 관리하였습니다.  
또한, Figma를 사용하여 게임 구조를 시각적으로 정리하고 팀원들과 공유하였습니다.  
<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Notion.png?raw=true" width="450"/>
<br /><br />
### **?? 개발 과정**  
개발 과정에서 팀원들은 정기적인 화상 회의를 통해 진행 상황을 공유하고, 발생한 문제를 함께 해결하였습니다.  
코드 리뷰를 통해 각자의 구현 방식을 이해하여 기능을 병합하는 과정을 원활하게 진행할 수 있었습니다.  
또한, 필요에 따라 추가적인 기능 개선 사항을 논의하고 적용하였습니다.  
<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Meeting.png?raw=true" width="450"/>
<br /><br />
### **?? 테스트 및 수정**  
개발 완료 후, 여러 차례 테스트를 진행하며 발견된 버그를 수정하고 게임의 완성도를 높였습니다.  
특히, 시스템적으로 발생하는 문제는 팀원들과 협력하여 개선 방안을 마련하고 적용하였습니다.  
<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/BugList.png?raw=true" width="450"/>  
<br /><br />
### **?? 프로젝트 완료**  
모든 기능이 정상적으로 작동하는 것을 확인한 후, 프로젝트를 성공적으로 마무리하였습니다.  
최종적으로 개발된 게임을 정리하며 팀원들과의 협업을 되돌아보고 이번 프로젝트를 통해 얻은 경험을 공유하였습니다.  

<br /><br />
### ?? 협업 툴
<div>
<img src="https://github.com/yewon-Noh/readme-template/blob/main/skills/Github.png?raw=true" width="80">
<img src="https://github.com/yewon-Noh/readme-template/blob/main/skills/Notion.png?raw=true" width="80">
<img src="https://github.com/yewon-Noh/readme-template/blob/main/skills/Figma.png?raw=true" width="80">
</div>

- GitHub: 코드 버전 관리 및 협업
- Notion: 프로젝트 문서 정리 및 일정 관리
- Figma: UI/UX 디자인 및 프로토타이핑

<br />

---

## ?? 기술적 이슈와 해결 과정 

### 문제 1: 호출 스택이 쌓이는 문제  
**?? 원인 분석**  
- 함

**?? 해결 방법**  
? 델
```
ESc
}
```

  <br /><br />

### 문제 2: D
**?? 원인 분석**  
- 기

**?? 해결 방법**  
? 데
```
i
```
<br />

---
