using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : Puzzle
{
    public Shelf[] Shelves;
    [System.Serializable]
    public struct Shelf
    {
        public GameObject[] books;
        public GameObject[] booksFinalOrder;
    }

    private GameObject selectedBook;
    private Shelf selectedShelf;

    public bool BookSelected(GameObject book)
    {
        Debug.Log($"{book.name} was selected");
        Debug.Log($"book width: {book.transform.lossyScale.x}");

        if (booksShelved) return false;

        if (!selectedBook) //first selection
        {
            selectedBook = book;
            bool shelfFound = false;
            foreach (Shelf shelf in Shelves)
            {
                foreach (GameObject shelfBook in shelf.books)
                {
                    if (book == shelfBook)
                    {
                        selectedShelf = shelf;
                        shelfFound = true;
                        break;
                    }
                }
                if (shelfFound) break;
            }

        }
        else //second selection
        {

            //check that book is on selected shelf
            // loop through selected shelf and if book is not in shelf then exit out of code
            bool bookOnShelf = false;
            foreach (GameObject ShelfBook in selectedShelf.books)
            {
                if (book == ShelfBook)
                {
                    bookOnShelf = true;
                }
            }

            if (!bookOnShelf)
            {
                Debug.Log("cannot swap books on different shelves");
                selectedBook = null;
                return false;
            }

            bool booksNeighbors = false;
            //check that books are next to eachother
            for (int i = 0; i < selectedShelf.books.Length - 1; i += 1)
            {
                if (selectedShelf.books[i] == selectedBook && selectedShelf.books[i + 1] == book || selectedShelf.books[i] == book && selectedShelf.books[i + 1] == selectedBook)
                {
                    booksNeighbors = true;
                    GameObject temp = selectedShelf.books[i];
                    selectedShelf.books[i] = selectedShelf.books[i + 1];
                    selectedShelf.books[i + 1] = temp;
                    break;
                }
            }
            if (!booksNeighbors)
            {
                Debug.Log("Cannot swap books");
                return false;
            }

            Transform b1 = selectedBook.transform; // left
            Transform b2; // right
            if (selectedBook.transform.position.x > book.transform.position.x)
            {
                b1 = book.transform;
                b2 = selectedBook.transform;
            }
            else
            {
                b2 = book.transform;
            }

            float w1 = b1.transform.lossyScale.x;
            float w2 = b2.transform.lossyScale.x;
            float x0 = b1.transform.position.x - w1 / 2;

            b2.transform.position = new Vector3(x0 + w2 / 2, b2.transform.position.y, b2.transform.position.z);

            b1.transform.position = new Vector3(x0 + w2 + w1 / 2, b1.transform.position.y, b1.transform.position.z);

            //swap book in the shelf.Books


            selectedBook = null;
            CheckBooksShelved();
        }
        return false;
    }

    private bool booksShelved = false;
    private void CheckBooksShelved()
    {
        bool shelved = true;
        foreach (Shelf shelf in Shelves)
        {
            GameObject leftBook;
            int bookPos = 0;
            for (int i = 0; i < shelf.booksFinalOrder.Length; i += 1)
            {
                leftBook = shelf.booksFinalOrder[i];
                if (leftBook == null) continue;
                while (leftBook != shelf.books[bookPos])
                {
                    bookPos += 1;
                }

                if (i < shelf.booksFinalOrder.Length - 1 && shelf.booksFinalOrder[i + 1] != null)
                {
                    if (bookPos == shelf.books.Length - 1 || shelf.booksFinalOrder[i + 1] != shelf.books[bookPos + 1])
                    {
                        shelved = false;
                        break;
                    }
                }
            }
            if (shelved == false || !shelved) break;
        }

        if (shelved)
        {
            booksShelved = true;
            FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }

    private void Start()
    {
        foreach (Shelf shelf in Shelves)
        {
            SortBookShelf(shelf);
        }
    }

    private void SortBookShelf(Shelf shelf)
    {
        for (int j = 0; j < shelf.books.Length; j += 1)
        {
            for (int i = 0; i < shelf.books.Length - 1; i += 1)
            {
                if (shelf.books[i].transform.position.x > shelf.books[i + 1].transform.position.x)
                {
                    GameObject temp = shelf.books[i];
                    shelf.books[i] = shelf.books[i + 1];
                    shelf.books[i + 1] = temp;
                }
            }
        }

    }
}
