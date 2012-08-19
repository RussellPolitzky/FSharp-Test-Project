module ReadFileAnimalsNumbers

open System
open System.Text
open StringUtils

// Inspired by Claudio Cherubino's 
// interview question http://www.fsharp.it/

let animals = @"Bird
Bovidae
Canidae
Equidae
Felidae
Fish
Aardvark
Albatross
Alligator
Alpaca
Buffalo
Ant
Anteater
Antelope
Ape
Armadillo
Donkey
Baboon
Badger
Barracuda
Bat
Bear
Beaver
Bee
Bison
Boar
Buffalo
Bush baby
Butterfly
Camel
Caribou
Cat
Caterpillar
Chamois
Cheetah
Chicken
Chimpanzee
Chinchilla
Clam
Cobra
Cockroach
Cod
Cormorant
Coyote
Crab
Crane
Crocodile
Crow
Deer
Dinosaur
Dog
Dogfish
Dolphin
Donkey
Dove
Dragonfly
Duck
Dugong
Eagle
Echidna
Eel
Eland
Elephant
Elephant seal
Elk (wapiti)
Emu
Falcon
Ferret
Finch
Fish
Fly
Fox
Frog
Gaur
Gazelle
Gerbil
Giant Panda
Giraffe
Gnat
Gnu
Goat
Goose
Gopher
Gorilla
Grasshopper
Grouse
Guanaco
Guinea fowl
Guinea pig
Gull
Hamster
Hare
Hawk
Hedgehog
Heron
Hippopotamus
Hornet
Horse
Human
Hummingbird
Hyena
Iguana
Jackal
Jaguar
Jay, Blue
Jellyfish
Kangaroo
Koala
Komodo dragon
Kouprey
Kudu
Lark
Lemur
Leopard
Lion
Llama
Lobster
Locust
Loris
Louse
Lyrebird
Magpie
Mallard
Manatee
Meerkat
Mink
Mole
Monkey
Moose
Mouse
Mosquito
Mule
Narwhal
Newt
Nightingale
Octopus
Okapi
Opossum
Oryx
Ostrich
Otter
Owl
Ox
Oyster
Panda
Panther
Parrot
Partridge
Peafowl
Pelican
Penguin
Pig
Pigeon
Platypus
Pony
Porcupine
Porpoise
Prairie Dog
Quelea
Rabbit
Raccoon
Rail
Ram
Rat
Raven
Red deer
Red panda
Reindeer
hinoceros
Rook
Salamander
Sand Dollar
Sea lion
Sea Urchin
Seahorse
Seal
Seastar
Serval
Shark
Sheep
Shrew
Skunk
Snail
Snake
Spider
Squid
Squirrel
Stinkbug
Swallow
Swan
Tapir
Tarsier
Termite
Tiger
Toad
Trout
Turkey
Turtle
Vicuña
Wallaby
Walrus
Wasp
Water buffalo
Weasel
Whale
Wolf
Wombat
Woodpecker
Worm
Wren
Yak
Worm
Wren
Yak
Zebra
Bird
Bovidae
Canidae
Equidae
Felidae
Fish
Aardvark
Albatross"

let numbers = @"1
3
54
6
7
2
34
5
35
36
7
4
4
4
4
4
9
0
12
34
56
789
7
356
36
356
3456"

/// Takes a file, splits it into lines
/// and returns a lambda function which 
/// when evaluated, returns a random 
/// line from the original file.
///
/// Note the use of the lambda and the 
/// closure here to capture the instance
/// of Random and the string array.
/// Closures can be very elegant.
let _randomString (strings:string)  = 
    let stringsArray = 
        strings |> split [|'\r'|]  
        |> Array.map (fun i -> i.Trim())
    let random = new Random()
    (fun () -> stringsArray.[random.Next(0, stringsArray.Length-1)])

// Note how the functions which capture the 
// list of animals and numbers are created.
let randomAnimal        = _randomString animals
let randomNumberStrings = _randomString numbers

// Simple discriminated union
type AnimalNumber = Animal | Number | None


/// Get a random list of animals and 
/// numbers to use to build the file.
let getAnimalsAndNumbers =
    let random = new Random()
    { 1..100 }
    |> Seq.map (fun _ -> match random.Next()%2 with
                         | 0 -> Animal, randomAnimal()
                         | _ -> Number, randomNumberStrings())


type private Accumulator = { mutable lastEntityType: AnimalNumber; strb: StringBuilder }


/// Build the file using fold ...
let getFile = 
    let accum = { lastEntityType = None; strb = new StringBuilder() }
    let typeHasChanged theAccumulator newItem = theAccumulator.lastEntityType <> (fst newItem)
    (Seq.fold (fun acc typeAndStr -> if (typeHasChanged acc typeAndStr) 
                                     then let typeString = sprintf "%A" (fst typeAndStr)
                                          acc.strb.AppendLine(typeString) |> ignore
                                     acc.strb.AppendLine(snd typeAndStr)  |> ignore
                                     acc.lastEntityType <- (fst typeAndStr)
                                     acc) 
                 accum
                 getAnimalsAndNumbers).strb |> toString |> trim

/// Now that we have the file, parse it




