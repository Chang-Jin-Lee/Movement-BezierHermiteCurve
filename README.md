# Shooter-BezierHermiteCurve
Shooter Game Using BezierCurve, HermiteCurve, Catmull-Rom Spline
Unity 2D 미니게임입니다. 
베지에 커브와 캣멀롬 스플라인을 활용하여 오브젝트 이동을 구현했습니다.
장애물을 피해 목표물에 도착해야합니다.



<div align="center">

 ![2025-07-2115-05-15-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/f4c980de-3461-4dac-b90a-fbe90b4c429f)

</div>

---

## 구현에서 중요하게 생각한 부분

- CatmullRomSpline을 사용한 곡선 움직임

  → 적의 움직임을 곡선으로 움직여서 플레이어가 예측하기 어렵게 만듬

- 적 스포너의 랜덤 위치, CatmullRomSpline 각 포인트의 적절한 랜덤 Position

  → 상하좌우,중앙에서 시작해 나아가는 곡선이 적절하게 생성됨

---

## 사용한 곡선

- **Cubic Bezier Curve**  
  → 장애물 이동 궤적에 사용

- **Hermite Curve**  
  → 오브젝트의 이동 및 회전에 사용

- **Catmull-Rom Spline**  
  → 연속된 여러 지점을 따라 자연스럽게 움직이도록 경로 생성

---

## 주요 기능

- 곡선을 따라 오브젝트 이동
- 게임 오버 및 재시작
- 하나의 씬(Scene1)에서 게임 진행

---

## 기본 조작

| 키 | 동작 |
|----|------|
| ← / → / ↑ / ↓ | 조준 방향 이동 |
| Space | 게임 오버 후 재시작 |

