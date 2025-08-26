using System;
using UnityEngine;

public class MobileInput : IInput
{
   public event Action OnRight;
   public event Action OnLeft;
   public event Action OnBack;
   public event Action OnRun;
    
   private Vector2 _fingerDownPosition;
   private Vector2 _fingerUpPosition;
   
   private const float _swipeThreshold = 20f;
   private bool _isTouch;

   public void Detect()
   {
      DetectTouch();
   }

   private void DetectTouch()
   {
      foreach (Touch touch in Input.touches)
      {
         _isTouch = false;

         if (touch.phase == TouchPhase.Began)
         {
            if (!_isTouch)
            {
               _fingerUpPosition = touch.position;
               _fingerDownPosition = touch.position;
               _isTouch = true;
            }
         }

         if (touch.phase == TouchPhase.Moved)
         {
            if (_isTouch)
            {
               _fingerDownPosition = touch.position;
               DetectSwipe();
            }
         }

         if (touch.phase == TouchPhase.Ended)
         {
            _fingerDownPosition = touch.position;
            DetectSwipe();
         }
      }
   }

   private void DetectSwipe()
   {
      if (VerticalMoveValue() > _swipeThreshold &&
          VerticalMoveValue() > HorizontalMoveValue())
      {
         if (_fingerDownPosition.y - _fingerUpPosition.y > 0)
            OnRun?.Invoke();

         else if (_fingerDownPosition.y - _fingerUpPosition.y < 0)
            OnBack?.Invoke();

         _fingerUpPosition = _fingerDownPosition;

      }
      else if (HorizontalMoveValue() > _swipeThreshold &&
               HorizontalMoveValue() > VerticalMoveValue())
      {
         if (_fingerDownPosition.x - _fingerUpPosition.x > 0)
            OnRight?.Invoke();

         else if (_fingerDownPosition.x - _fingerUpPosition.x < 0)
            OnLeft?.Invoke();

         _fingerUpPosition = _fingerDownPosition;
      }
   }

   private float VerticalMoveValue()
   {
      return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
   }

   private float HorizontalMoveValue()
   {
      return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
   }
}