using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject customCursorPrefab; // Prefab của GameObject tùy chỉnh (ví dụ: 3D model)
    private GameObject customCursorInstance; // Thể hiện của GameObject tùy chỉnh

    void Start() {
        // Khởi tạo thể hiện của GameObject tùy chỉnh
        customCursorInstance = Instantiate(customCursorPrefab);
        customCursorInstance.SetActive(false);

        // Ẩn con chuột mặc định của hệ thống
        Cursor.visible = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Khi con chuột trỏ vào
        customCursorInstance.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Khi con chuột rời khỏi
        customCursorInstance.SetActive(false);
    }
}
