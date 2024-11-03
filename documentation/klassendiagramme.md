```mermaid

classDiagram
    UserBase <|-- Admin
    Reservation <|-- User
    Reservation <|-- Table
    Restaurant <|-- ReservationSystem 
    UserBase <|-- ReservationSystem
    Admin <|-- ReservationSystem
    Room <|-- Restaurant
    RestaurantOwner <|-- Restaurant
    UserBase <|-- RestaurantOwner
    UserBase <|-- User
    class Admin{
      +String firstName
      +String lastName
      +String email
      +String password
      +createRetaurant()
      +deleteRestaurant()
    }
    class Reservation{
      -User creator
      -Table table
      -DateTime startTime
      -DateTime endTime
      +User Creator(get;)
      +Table Table(get; set;)
      +long startTime(get;)
      +long Endtime(get;)
    }

    class ReservationSystem{
        -List<Restaurant> restautrants
        -List<UserBase> users
        -Admin admin
        +bool AddRestaurant(Restaurant r)
        +bool RemoveRestaurant(Restaurant r)
        +bool AddUser(UserBase user)
        +bool RemoveUser(UserBase user)
        +List<Restaurant> Restaurants(get;)
        +List<Userbase> Users(get;)
    }

    class Restaurant{
        -String name
        -String address
        -List<Room> rooms
        -RestaurantOwner owner
        +get name()
        +set name()
        +get Address()
        +set Address()
        +get Rooms()
        +get RestaurantOwner()
        +set RestaurantOwner()
    }

    class RestaurantOwner{
        +String firstName
        +String lastName
        +String email
        +String password
        +bool AddRoom(ref Restaurant, Room r)
        +bool RemoveRoom(ref Restaurant restautrant, Room r)
        +bool AddTable(ref Room room, Table t)
        +bool removeTable(ref Room room, Table t)
    }

    class Room{
        -List<Table> tables
        -int roomNumber
        +get tables()
        +set tables()
        +get RoomNumber()
        +set RoomNumber()
    }

    class Table{
        -int chairs
        -TableAttributes attributes
        -int tabeleID
        -List<Reservation> reservations
        +static int Reserve(ref List<Table> tables, ref Reservation reservation)
        +get chairs()
        +get attributes()
        +get tabeleID()
        +get reservations()
    }

    class User{
        +String firstName
        +String lastName
        +String email
        +String password
        +User()
    }

    class UserBase{
        -String firstName
        -String lastName
        -String email
        -String passwordHash
        +bool isCorrectPassword(String password)
        +get firstName()
        +set firstName()
        +get lastName()
        +set lastName()
        +get email()
        +set email()
        +get passwordHash()
        +set passwordHash()
    }
    
