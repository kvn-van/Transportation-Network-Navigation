﻿//2024 CAB301 Assignment 3 
//TransportationNetwok.cs
//Assignment3B-TransportationNetwork

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class TransportationNetwork
{

    private string[]? intersections; //array storing the names of those intersections in this transportation network design
    private int[,]? distances; //adjecency matrix storing distances between each pair of intersections, if there is a road linking the two intersections

    public string[]? Intersections
    {
        get {return intersections;}
    }

    public int[,]? Distances
    {
        get { return distances; }
    }


    //Read information about a transportation network plan into the system
    //Preconditions: The given file exists at the given path and the file is not empty
    //Postconditions: Return true, if the information about the transportation network plan is read into the system, the intersections are stored in the class field, intersections, and the distances of the links between the intersections are stored in the class fields, distances;
    //                otherwise, return false and both intersections and distances are null.
    public bool ReadFromFile(string filePath){
        try{
            var lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length == 0){
                return false;
            }
    
            var intersectionSet = new HashSet<string>();
            var edges = new List<Tuple<string, string, int>>();
    
            // If format is not correct (not 3 columns) return false
            foreach (var line in lines){
                var parts = line.Split(',');
                if (parts.Length != 3){
                    return false; 
                }
    
                var intersection1 = parts[0].Trim();
                var intersection2 = parts[1].Trim();
                if (!int.TryParse(parts[2].Trim(), out var distance)){
                    return false; // Invalid distance format
                }
    
                intersectionSet.Add(intersection1);
                intersectionSet.Add(intersection2);
                edges.Add(new Tuple<string, string, int>(intersection1, intersection2, distance));
            }
    
            // Convert set to list
            intersections = intersectionSet.ToArray();
            Array.Sort(intersections); // Optional: sort intersections for consistency
    
            // Initialize distance matrix
            var n = intersections.Length;
            distances = new int[n, n];
    
            // Initialize distances to a large number (assuming no negative distances)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = i == j ? 0 : int.MaxValue;
                }
            }
    
            // Fill distance matrix with the edges
            foreach (var edge in edges)
            {
                var fromIndex = Array.IndexOf(intersections, edge.Item1);
                var toIndex = Array.IndexOf(intersections, edge.Item2);
                distances[fromIndex, toIndex] = edge.Item3;
            }
    
            return true;
        }
        catch (Exception)
        {
            intersections = null;
            distances = null;
            return false;
        }
    }




    //Display the transportation network plan with intersections and distances between intersections
    //Preconditions: The given file exists at the given path and the file is not empty
    //Postconditions: The transportation netork is displayed in a matrix format
    public void DisplayTransportNetwork()
    {
        Console.Write("       ");
        for (int i = 0; i < intersections?.Length; i++)
        {
                    Console.Write(intersections[i].ToString().PadRight(5) + "  ");
        }
        Console.WriteLine();


        for (int i = 0; i < distances?.GetLength(0); i++)
        {
            Console.Write(intersections[i].ToString().PadRight(5) + "  ");
            for (int j = 0; j < distances?.GetLength(1); j++)
            {
                if (distances[i, j] == Int32.MaxValue)
                    Console.Write("INF  " + "  ");
                else
                    Console.Write(distances[i, j].ToString().PadRight(5)+"  ");
            }
            Console.WriteLine();
        }
    }


    //Check if this transportation network is strongly connected. A transportation network is strongly connected, if there is a path from any intersection to any other intersections in thihs transportation network. 
    //Precondition: Transportation network plan data have been read into the system.
    //Postconditions: return true, if this transpotation netork is strongly connected; otherwise, return false. This transportation network remains unchanged.
    public bool IsConnected(){
        if (intersections == null || distances == null){
            return false;
        }

        int n = intersections.Length;
        bool[] visited = new bool[n];
        Stack<int> stack = new Stack<int>();

        // Step 1: Perform DFS to fill stack with vertices in the order of their finishing times
        for (int i = 0; i < n; i++){
            if (!visited[i]){
                DFS(i, visited, stack);
            }
        }

        // Step 2: Reverse the graph
        int[,] reversedDistances = new int[n, n];
        for (int i = 0; i < n; i++){
            for (int j = 0; j < n; j++){
                reversedDistances[j, i] = distances[i, j];
            }
        }

        // Step 3: Perform DFS on the reversed graph in the order of decreasing finishing times
        visited = new bool[n];
        int stronglyConnectedCount = 0;

        while (stack.Count > 0){
            int v = stack.Pop();
            if (!visited[v]){
                DFSReversed(v, visited, reversedDistances);
                stronglyConnectedCount++;
                if (stronglyConnectedCount > 1){
                    return false;
                }
            }
        }
        return true;
    }

    private void DFS(int v, bool[] visited, Stack<int> stack){
        visited[v] = true;

        for (int i = 0; i < distances?.GetLength(1); i++){
            if (distances[v, i] != int.MaxValue && !visited[i]){
                DFS(i, visited, stack);
            }
        }

        stack.Push(v);
    }

    private void DFSReversed(int v, bool[] visited, int[,] reversedDistances){
        visited[v] = true;

        for (int i = 0; i < reversedDistances.GetLength(1); i++){
            if (reversedDistances[v, i] != int.MaxValue && !visited[i]){
                DFSReversed(i, visited, reversedDistances);
            }
        }
    }

    
    
    //Find the shortest path between a pair of intersections
    //Precondition: transportation network plan data have been read into the system
    //Postcondition: return the shorest distance between two different intersections; return 0 if there is no path from startVerte to endVertex; returns -1 if startVertex or endVertex does not exists. This transportation network remains unchanged.

    public int FindShortestDistance(string startVertex, string endVertex)
    {
        //To be completed by students
        return 0; //to be removed

    }


    //Find the shortest path between all pairs of intersections
    //Precondition: transportation network plan data have been read into the system
    //Postcondition: return the shorest distances between between all pairs of intersections through a two-dimensional int array and this transportation network remains unchanged

    public int[,] FindAllShortestDistances()
    {
        //To be completed by students
        return null; //to be removed
    }
}