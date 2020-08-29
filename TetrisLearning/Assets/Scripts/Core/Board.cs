using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform m_emptySprite;
    public int m_width = 10;
    public int m_height = 30;
    public int m_header = 8;
    Transform[,] grid;
    private List<Transform> Grid = new List<Transform>();
 
    public ParticlePlayer[] RowClearParticle = new ParticlePlayer[4];
    public ParticlePlayer DeleteRowParticle;

    public int clearedrows;
    public int GetWidth()
    {
        return m_width;
    }
    void Awake()
    {
        grid = new Transform[m_width, m_height];
    }
    // Start is called before the first frame update
    void Start()
    {
        //DrawEmptyBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }
    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorRound.Round(child.position);
            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }
            if (IsOccupied((int)pos.x, (int)pos.y, shape))
            {
                return false;
            }
        }
        return true;
    }
     public async Task DrawEmptyBoard()
    {
        
        if (m_emptySprite)
        {
            int col = 0;
            int row = 0;
            int dx = 1;
            int dy = 0;
            int dirChanges = 0;
            int visits = m_height - m_header;

            for (int i = 0; i < (m_height - m_header) * m_width; i++)
            {
                Transform clone;
                clone = Instantiate(m_emptySprite, new Vector3(row, col, 0), Quaternion.identity) as Transform;
                clone.name = "Board Space ( x =" + row.ToString() + " , y =" + col.ToString() + " )";
                clone.transform.parent = transform;
                Grid.Insert(i,clone);
                await Task.Delay(5);
                if (--visits == 0)
                {
                    visits = (m_height - m_header) * (dirChanges % 2) + m_width * ((dirChanges + 1) % 2) - (dirChanges / 2 - 1) - 2;
                    int temp = dx;
                    dx = -dy;
                    dy = temp;
                    dirChanges++;
                }

                col += dx;
                row += dy;
            }
        }
        else
        {
            Debug.Log("Warning! Please assign the emptySprite object");
        }
    }
    public void DeleteGrid()
    {
        foreach(Transform child in Grid)
        {
            Destroy(child.gameObject);
        }
        Grid.Clear();
    }
    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
        {
            return;
        }
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorRound.Round(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }
    bool IsOccupied(int x, int y, Shape shape)
    {
        return (grid[x, y] != null && grid[x, y].parent != shape.transform);
    }
    bool IsComplete(int y)
    {
        for (int x = 0; x < m_width; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void ClearRow(int y)
    {
        DeleteRow(y);
        ShiftRowsDown(y);
        DeleteRowParticle.transform.position = new Vector3(0, y, -1);
        DeleteRowParticle.Play();
    }
    void OneRowDown(int y)
    {
        for (int x = 0; x < m_width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    void ShiftRowsDown(int y)
    {
        for (int i = y; i < m_height; ++i)
        {
            OneRowDown(i);
        }
        
    }
    public void ClearAllRows()
    {
        clearedrows = 0;
        for (int y = 0; y < m_height; y++)
        {
            if (IsComplete(y))
            {
                ClearRowFx(clearedrows, y);
                clearedrows++;
            }
        }

        for (int y = 0; y < m_height; y++)
        {
            if (IsComplete(y))
            {
                DeleteRow(y);
                ShiftRowsDown(y + 1);
                y--;
            }
        }
    }
    public bool TopLimit(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= (m_height - m_header - 1))
            {
                return true;
            }
        }
        return false;
    }
    void ClearRowFx(int indx, int y)
    {
        if (RowClearParticle[indx])
        {
            RowClearParticle[indx].transform.position = new Vector3(0, y, -1);
            RowClearParticle[indx].Play();
        }
    }

    public void GameOverClear()
    {
        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; ++x)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                }
                grid[x, y] = null;
            }
        }
    }
    void DeleteRow(int y)
    {
        for (int x = 0; x < m_width; ++x)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
            grid[x, y] = null;
        }
    }
    
}

