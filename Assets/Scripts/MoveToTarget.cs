using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float moveSpeed = 5f;  // Tốc độ di chuyển của đối tượng A
    private Vector3 targetPosition;  // Vị trí mục tiêu
    private bool isMoving = false;  // Biến để kiểm tra xem đối tượng A đang di chuyển hay không

    void Update()
    {
        // Kiểm tra khi nhấn chuột trái
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Lấy vị trí chuột trong thế giới 2D
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;  // Đảm bảo Z = 0 để di chuyển trong không gian 2D

            // Kiểm tra xem chuột có nhấn vào đối tượng B hoặc C không
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject targetObject = hit.collider.gameObject;
                if (targetObject.CompareTag("Watch") || targetObject.CompareTag("Shoes") || targetObject.CompareTag("Bag") || targetObject.CompareTag("Hat"))
                {
                    // Lấy vị trí mục tiêu từ đối tượng B hoặc C
                    targetPosition = targetObject.transform.position;

                    // Bắt đầu di chuyển đối tượng A đến vị trí mục tiêu
                    StartCoroutine(MoveToTargetPosition(targetPosition));
                }
            }
        }
    }

    private System.Collections.IEnumerator MoveToTargetPosition(Vector3 target)
    {
        isMoving = true;

        while (transform.position != target)
        {
            // Di chuyển đối tượng A đến vị trí mục tiêu với tốc độ được xác định
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
