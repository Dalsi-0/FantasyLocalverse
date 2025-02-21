<<<<<<< Updated upstream
# FantasyLocalverse
 
=======
﻿ <div align="center">

## 판타지 로컬버스 (Fantasy Localverse)

[<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=Github&logoColor=white" />]()
<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.17~2025.02.20-73abf0?style=flat&logo=&logoColor=white" />]()

---
</div> 

## 📝 프로젝트 소개

Unity로 제작한 간단한 게임입니다.
플레이어는 마을을 돌아다니며 NPC와 상호작용하고, 카메라 연출을 경험하며, 미니 게임을 플레이할 수 있습니다.
미니 게임은 총 두 가지로, 하나는 유명한 플래피 버드를 재현한 게임이며, 다른 하나는 빛을 활용하여 많은 가짜 물건 중에 진짜를 찾는 간단한 미니 게임입니다.

<br />  

---

## 🎮 게임 기능 개요

| 기능 | 설명 |
|---|---|
| 🏙️ **마을 탐험** | 플레이어는 마을을 돌아다니며 NPC와 상호작용하고, 다양한 장소를 방문할 수 있습니다. |
| 🎥 **카메라 연출** | 특정 상호작용을 통해 마을을 한눈에 살펴볼 수 있는 카메라 연출을 경험할 수 있습니다. |
| 👗 **캐릭터 의상 교체** | 다양한 의상을 선택하여 캐릭터의 외형을 변경할 수 있습니다. |
| 🎮 **미니 게임** | 플래피 버드와 빛을 활용한 퍼즐형 미니 게임을 플레이할 수 있습니다. |

<br />  

---

## 📸 화면 구성
|마을|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Main.png?raw=true" width="700"/>|
|마을에서 다양한 활동을 시작할 수 있습니다.|

<br /><br />

|상호작용-대화|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Battle.png?raw=true" width="700"/>|
|던전의 층마다 다른 종류의 몬스터들과의 전투가 벌어집니다.|  

<br /><br />

|시네머신-타임라인|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Inventory.png?raw=true" width="700"/>|
|인벤토리에서 획득한 장비를 확인하고 장착/해제할 수 있습니다.|

<br /><br />

|미니 게임|
|:---:|
|<img src="https://github.com/yndoo/Dun9eonAndFi9ht/blob/main/ReadmeImage/Quest.png?raw=true" width="700"/>|
|퀘스트를 수락하고 진행 상황을 확인할 수 있습니다.|

<br />   

---

## 📂 프로젝트 폴더 구조
```
??Dun9eonAndFi9ht
 ┣ ??Dun9eonAndFi9ht
 ┃ ┣ ??App
 ┃ ┃ ┗ ??Program.cs
 ┃ ┣ ??Characters
 ┃ ┃ ┣ ??Character.cs
 ┃ ┃ ┣ ??Monster.cs
```

<br />  

---

## 🤔 기술적 이슈와 해결 과정 

### 문제 : 리포지토리 패턴을 활용한 씬 전환 및 데이터 관리 개선
**📍 원인 분석**  
- 각 Manager의 역할 과다: 매니저 클래스가 데이터 관리까지 담당하며 과도한 책임이 집중되었습니다.  
- 매니저 간 결합도 문제: 여러 매니저 클래스가 서로 직접 참조하면서 결합도가 높아 유지보수가 어려웠습니다.  
- 씬 전환 시 객체 참조 문제: 씬 전환 시 DontDestroyOnLoad로 유지되는 객체가 있는 반면, 일부 객체는 삭제되면서 NullReferenceException 오류가 발생했습니다.  
- Player 상태 유지 어려움: Player가 각 씬마다 개별 배치되어 상태 유지가 어렵고, 데이터 공유가 원활하지 않았습니다.  

**💡 해결 방법**  
✔ 책임 분리: Manager가 직접 데이터 관리를 담당하지 않도록 리포지토리 패턴을 적용하여, 데이터는 각각의 Repository에서 관리하도록 변경했습니다.  
✔ 매니저 간 결합도 감소: 각 매니저가 직접 참조하던 객체를 리포지토리를 통해 접근하도록 수정하여 클래스 간 의존성을 낮추고 유지보수를 용이하게 했습니다.  
✔ 객체 참조 문제 해결: 리포지토리 패턴을 적용하여 DontDestroyOnLoad를 적용할 객체와 씬 전환 시 재생성할 객체를 분리하여 NullReferenceException이 발생하지 않도록 개선했습니다.  
✔ Player 상태 유지 개선: Player를 씬에 직접 배치하는 대신 GameRepository에서 프리팹과 위치 정보를 불러와 동적으로 생성하도록 변경하여 씬 전환 후에도 일관된 상태를 유지할 수 있도록 했습니다.  

**🎯 결과 및 개선 효과**  
✅ 책임 분리: Manager 클래스의 역할을 최소화하고, 데이터 관리를 리포지토리에서 담당하도록 변경하여 유지보수성을 향상시켰습니다.  
✅ 확장성 증가: 새로운 데이터를 추가하거나 변경할 때 기존 코드를 수정할 필요 없이 리포지토리만 수정하면 되도록 구조를 개선했습니다.  
✅ 버그 감소: 씬 변경 시 객체 참조 방식이 명확해져 NullReferenceException 발생 가능성이 줄어들었으며 안정적인 데이터 관리가 가능해졌습니다.  

<br />  

---

## 🕹️ 플레이 링크  
**👉 [던전 앤 파이트 플레이하기](https://play.unity.com/en/games/9a6104fa-1051-4c4a-970a-875c5ab6e126/fantasylocalverse)**

웹 브라우저에서 바로 플레이할 수 있습니다! 🎮   

<br />  

---
>>>>>>> Stashed changes
