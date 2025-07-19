# Shooter-BezierHermiteCurve
Shooter Game Using BezierCurve, HermiteCurve, Catmull-Rom Spline
Unity 기반의 2D 슈터 미니게임입니다.  
다양한 곡선 수학 기법을 활용하여 오브젝트 이동과 탄환 궤적을 구현했습니다.

---

## 사용한 곡선

- **Cubic Bezier Curve**  
  → 탄환(Bullet) 발사 궤적에 사용

- **Hermite Curve**  
  → 런타임 중 오브젝트의 이동 및 회전에 사용

- **Catmull-Rom Spline**  
  → 연속된 여러 지점을 따라 자연스럽게 움직이도록 경로 생성

---

## 주요 기능

- 곡선을 따라 움직이는 탄환 발사
- 오브젝트 회전 처리
- 타겟과의 충돌 판정
- 게임 오버 및 재시작 로직 포함
- 하나의 씬(Scene1)에서 게임 진행

---

## 기본 조작

| 키 | 동작 |
|----|------|
| ← / → | 조준 방향 이동 |
| Space | 곡선 탄환 발사 |
| R | 게임 오버 후 재시작 |

